﻿using System;
using System.Collections.Generic;
using System.IO;
#if !MONOTOUCH
using System.Runtime.Serialization.Json;
#endif
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using ServiceStack.Text.Common;

namespace ServiceStack.Text.Tests.JsonTests
{
	public interface ICat
	{
		string Name { get; set; }
	}

	public interface IDog
	{
		string Name { get; set; }
	}

	//[KnownType(typeof(Dog))]
	//[KnownType(typeof(Cat))]
	public abstract class Animal
	{
		public abstract string Name
		{
			get;
			set;
		}
	}

	public class Dog : Animal, IDog
	{
		public override string Name { get; set; }

		public string DogBark { get; set; }
	}

    public class Collie : Dog
    {
        public bool IsLassie { get; set; }
    }

	public class Cat : Animal, ICat
	{
		public override string Name { get; set; }

		public string CatMeow { get; set; }
	}

	public class Zoo
	{
		public Zoo()
		{
			Animals = new List<Animal>
			{
				new Dog { Name = @"Fido", DogBark = "woof" },
				new Cat { Name = @"Tigger", CatMeow = "meow" },
			};
		}

		public List<Animal> Animals
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
	}

	[TestFixture]
	public class PolymorphicListTests : TestBase
	{
		String assemblyName;
		[SetUp]
		public void SetUp()
		{
			JsConfig.Reset();
			JsConfig<ICat>.ExcludeTypeInfo = false;
			assemblyName = GetType().Assembly.GetName().Name;
		}

		[Test]
		public void Can_serialise_polymorphic_list()
		{
			var list = new List<Animal>
			{
				new Dog { Name = @"Fido", DogBark = "woof" },
				new Cat { Name = @"Tigger", CatMeow = "meow" },
			};

			var asText = JsonSerializer.SerializeToString(list);

			Log(asText);

			Assert.That(asText,
				Is.EqualTo(
					"[{\"__type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\",\"DogBark\":\"woof\"},{\"__type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\",\"CatMeow\":\"meow\"}]"));
		}

		[Test]
		public void Can_serialise_an_entity_with_a_polymorphic_list()
		{
			var zoo = new Zoo {
				Name = @"City Zoo"
			};

			string asText = JsonSerializer.SerializeToString(zoo);

			Log(asText);

			Assert.That(
				asText,
				Is.EqualTo(
					"{\"Animals\":[{\"__type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\",\"DogBark\":\"woof\"},{\"__type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\",\"CatMeow\":\"meow\"}],\"Name\":\"City Zoo\"}"));
		}

		[Test]
		public void Can_serialise_polymorphic_entity_with_customised_typename()
		{
			try
			{
				JsConfig.TypeWriter = type => type.Name;

				Animal dog = new Dog { Name = @"Fido", DogBark = "woof" };
				var asText = JsonSerializer.SerializeToString(dog);

				Log(asText);

				Assert.That(asText,
					Is.EqualTo(
						"{\"__type\":\"Dog\",\"Name\":\"Fido\",\"DogBark\":\"woof\"}"));
			} finally {
				JsConfig.Reset();
			}
		}

		[Test]
		public void Can_deserialise_polymorphic_list()
		{
			var list =
				JsonSerializer.DeserializeFromString<List<Animal>>(
					"[{\"__type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\"},{\"__type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\"}]");

			Assert.That(list.Count, Is.EqualTo(2));

			Assert.That(list[0].GetType(), Is.EqualTo(typeof(Dog)));
			Assert.That(list[1].GetType(), Is.EqualTo(typeof(Cat)));

			Assert.That(list[0].Name, Is.EqualTo(@"Fido"));
			Assert.That(list[1].Name, Is.EqualTo(@"Tigger"));
		}

#if !MONOTOUCH
		[Test]
		public void Can_deserialise_polymorphic_list_serialized_by_datacontractjsonserializer()
		{
		    Func<string, Type> typeFinder = value => {
		        var regex = new Regex(@"^(?<type>[^:]+):#(?<namespace>.*)$");
		        var match = regex.Match(value);
		        var typeName = string.Format("{0}.{1}", match.Groups["namespace"].Value, match.Groups["type"].Value.Replace(".", "+"));
		        return Type.GetType(typeName);
		    };

		    try {
		        var originalList = new List<Animal> {new Dog {Name = "Fido"}, new Cat {Name = "Tigger"}};

		        var dataContractJsonSerializer = new DataContractJsonSerializer(typeof (List<Animal>), new[] {typeof (Dog), typeof (Cat)}, int.MaxValue, true, null, true);
		        JsConfig.TypeFinder = typeFinder;
		        List<Animal> deserializedList = null;
		        using (var stream = new MemoryStream()) {
		            dataContractJsonSerializer.WriteObject(stream, originalList);
		            stream.Position = 0;
		            using (var reader = new StreamReader(stream)) {
		                var json = reader.ReadToEnd();
		                deserializedList = JsonSerializer.DeserializeFromString<List<Animal>>(json);
		            }
		        }

		        Assert.That(deserializedList.Count, Is.EqualTo(originalList.Count));

		        Assert.That(deserializedList[0].GetType(), Is.EqualTo(originalList[0].GetType()));
		        Assert.That(deserializedList[1].GetType(), Is.EqualTo(originalList[1].GetType()));

		        Assert.That(deserializedList[0].Name, Is.EqualTo(originalList[0].Name));
		        Assert.That(deserializedList[1].Name, Is.EqualTo(originalList[1].Name));
		    } finally {
		        JsConfig.Reset();
		    }
		}
#endif

	    public void Can_deserialise_polymorphic_list_with_nonabstract_base()
		{
			var list =
				JsonSerializer.DeserializeFromString<List<Dog>>(
					"[{\"__type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\"},{\"__type\":\""
					+ typeof(Collie).ToTypeString()
					+ "\",\"Name\":\"Lassie\",\"IsLassie\":true}]");

			Assert.That(list.Count, Is.EqualTo(2));

			Assert.That(list[0].GetType(), Is.EqualTo(typeof(Dog)));
			Assert.That(list[1].GetType(), Is.EqualTo(typeof(Collie)));

			Assert.That(list[0].Name, Is.EqualTo(@"Fido"));
			Assert.That(list[1].Name, Is.EqualTo(@"Lassie"));
		}

		[Test]
	    public void Can_deserialise_polymorphic_item_with_nonabstract_base_deserializes_derived_properties_correctly()
		{
			var collie =
				JsonSerializer.DeserializeFromString<Dog>(
					"{\"__type\":\""
					+ typeof(Collie).ToTypeString()
					+ "\",\"Name\":\"Lassie\",\"IsLassie\":true}");

			Assert.That(collie.GetType(), Is.EqualTo(typeof(Collie)));
			Assert.That(collie.Name, Is.EqualTo(@"Lassie"));
			Assert.That(((Collie)collie).IsLassie, Is.True);
		}

		[Test]
		public void Can_deserialise_an_entity_containing_a_polymorphic_list()
		{
			var zoo =
				JsonSerializer.DeserializeFromString<Zoo>(
					"{\"Animals\":[{\"__type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\"},{\"__type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\"}],\"Name\":\"City Zoo\"}");

			Assert.That(zoo.Name, Is.EqualTo(@"City Zoo"));

			var animals = zoo.Animals;

			Assert.That(animals[0].GetType(), Is.EqualTo(typeof(Dog)));
			Assert.That(animals[1].GetType(), Is.EqualTo(typeof(Cat)));

			Assert.That(animals[0].Name, Is.EqualTo(@"Fido"));
			Assert.That(animals[1].Name, Is.EqualTo(@"Tigger"));
		}

#if !MONOTOUCH
		[Test]
		public void Can_deserialise_an_entity_containing_a_polymorphic_property_serialized_by_datacontractjsonserializer()
		{
		    Func<string, Type> typeFinder = value => {
                    var regex = new Regex(@"^(?<type>[^:]+):#(?<namespace>.*)$");
		            var match = regex.Match(value);
		            var typeName = string.Format("{0}.{1}", match.Groups["namespace"].Value, match.Groups["type"].Value.Replace(".", "+"));
		            return Type.GetType(typeName);
		        };

            try {
                var originalPets = new Pets {Cat = new Cat {Name = "Tigger"}, Dog = new Dog {Name = "Fido"}};

		        var dataContractJsonSerializer = new DataContractJsonSerializer(typeof (Pets), new[] {typeof (Dog), typeof (Cat)}, int.MaxValue, true, null, true);
                JsConfig.TypeFinder = typeFinder;
		        Pets deserializedPets = null;
		        using (var stream = new MemoryStream()) {
                    dataContractJsonSerializer.WriteObject(stream, originalPets);
		            stream.Position = 0;
                    using (var reader = new StreamReader(stream)) {
                        var json = reader.ReadToEnd();
		                deserializedPets = JsonSerializer.DeserializeFromString<Pets>(json);
                    }
		        }

			    Assert.That(deserializedPets.Cat.GetType(), Is.EqualTo(originalPets.Cat.GetType()));
			    Assert.That(deserializedPets.Dog.GetType(), Is.EqualTo(originalPets.Dog.GetType()));

			    Assert.That(deserializedPets.Cat.Name, Is.EqualTo(originalPets.Cat.Name));
			    Assert.That(deserializedPets.Dog.Name, Is.EqualTo(originalPets.Dog.Name));
            } finally {
                JsConfig.Reset();
            }
		}
#endif

		[Test]
		public void Can_deserialise_an_entity_containing_a_polymorphic_property_serialized_by_newtonsoft()
		{
			var json =
					"{\"$type\":\""
                    + typeof(Pets).ToTypeString()
					+ "\",\"Dog\":{\"$type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\"},\"Cat\":{\"$type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\"}}";
            try {
                //var originalPets = new Pets {Cat = new Cat {Name = "Tigger"}, Dog = new Dog {Name = "Fido"}};

                //var newtonsoftSerializer = new Newtonsoft.Json.JsonSerializer { TypeNameHandling = TypeNameHandling.All };
                //var buffer = new StringBuilder();
                //using (var writer = new StringWriter(buffer)) {
                //    newtonsoftSerializer.Serialize(writer, originalPets);
                //}
                //var json = buffer.ToString();
                JsConfig.TypeAttr = "$type";
		        var deserializedPets = JsonSerializer.DeserializeFromString<Pets>(json);

			    Assert.That(deserializedPets.Cat.GetType(), Is.EqualTo(typeof(Cat)));
			    Assert.That(deserializedPets.Dog.GetType(), Is.EqualTo(typeof(Dog)));

			    Assert.That(deserializedPets.Cat.Name, Is.EqualTo("Tigger"));
			    Assert.That(deserializedPets.Dog.Name, Is.EqualTo("Fido"));
            } finally {
                JsConfig.Reset();
            }
		}

		[Test]
		public void Can_deserialise_polymorphic_list_serialized_by_newtonsoft()
		{
            var json = 
					"[{\"$type\":\""
					+ typeof(Dog).ToTypeString()
					+ "\",\"Name\":\"Fido\"},{\"$type\":\""
					+ typeof(Cat).ToTypeString()
					+ "\",\"Name\":\"Tigger\"}}]";

            try {
		        var originalList = new List<Animal> {new Dog {Name = "Fido"}, new Cat {Name = "Tigger"}};

                //var newtonsoftSerializer = new Newtonsoft.Json.JsonSerializer { TypeNameHandling = TypeNameHandling.All };
                //var buffer = new StringBuilder();
                //using (var writer = new StringWriter(buffer)) {
                //    newtonsoftSerializer.Serialize(writer, originalList);
                //}
                //var json = buffer.ToString();

                JsConfig.TypeAttr = "$type";
		        var deserializedList = JsonSerializer.DeserializeFromString<List<Animal>>(json);

			    Assert.That(deserializedList.Count, Is.EqualTo(originalList.Count));

			    Assert.That(deserializedList[0].GetType(), Is.EqualTo(originalList[0].GetType()));
			    Assert.That(deserializedList[1].GetType(), Is.EqualTo(originalList[1].GetType()));

			    Assert.That(deserializedList[0].Name, Is.EqualTo(originalList[0].Name));
			    Assert.That(deserializedList[1].Name, Is.EqualTo(originalList[1].Name));
            } finally {
                JsConfig.Reset();
            }
		}

		public class Pets
		{
			public ICat Cat { get; set; }
			public IDog Dog { get; set; }
		}

		public class ExplicitPets
		{
			public Cat Cat { get; set; }
			public Dog Dog { get; set; }
		}

		[Test]
		public void Can_force_specific_TypeInfo()
		{
			JsConfig<Dog>.IncludeTypeInfo = true;
			var pets = new ExplicitPets()
			{
				Cat = new Cat { Name = "Cat" },
				Dog = new Dog { Name = "Dog" },
			};

			Assert.That(pets.ToJson(), Is.EqualTo(
				@"{""Cat"":{""Name"":""Cat""},""Dog"":{""__type"":""ServiceStack.Text.Tests.JsonTests.Dog, " + assemblyName + @""",""Name"":""Dog""}}"));

			Assert.That(new Dog { Name = "Dog" }.ToJson(), Is.EqualTo(
				@"{""__type"":""ServiceStack.Text.Tests.JsonTests.Dog, " + assemblyName + @""",""Name"":""Dog""}"));
		}

		[Test]
		public void Can_exclude_specific_TypeInfo()
		{
			JsConfig<ICat>.ExcludeTypeInfo = true;
			var pets = new Pets {
				Cat = new Cat { Name = "Cat" },
				Dog = new Dog { Name = "Dog" },
			};

			Assert.That(pets.ToJson(), Is.EqualTo(
				@"{""Cat"":{""Name"":""Cat""},""Dog"":{""__type"":""ServiceStack.Text.Tests.JsonTests.Dog, " + assemblyName + @""",""Name"":""Dog""}}"));
		}

		public class PetDog
		{
			public IDog Dog { get; set; }
		}

		public class WeirdCat
		{
			public Cat Dog { get; set; }
		}

		[Test]
		public void Can_read_as_Cat_from_Dog_with_typeinfo()
		{
			var petDog = new PetDog { Dog = new Dog { Name = "Woof!" } };
			var json = petDog.ToJson();

			Console.WriteLine(json);

			var weirdCat = json.FromJson<WeirdCat>();

			Assert.That(weirdCat.Dog, Is.Not.Null);
			Assert.That(weirdCat.Dog.Name, Is.EqualTo(petDog.Dog.Name));
		}

	}
}