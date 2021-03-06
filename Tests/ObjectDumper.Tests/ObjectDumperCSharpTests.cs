﻿using System.Collections.Generic;
using System.Diagnostics.Tests.Testdata;
using System.Diagnostics.Tests.Utils;
using System.Globalization;
using System.Linq;
using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace System.Diagnostics.Tests
{
    [Collection(TestCollections.CultureSpecific)]
    public class ObjectDumperCSharpCSharpTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ObjectDumperCSharpCSharpTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldDumpObject()
        {
            // Arrange
            var person = PersonFactory.GetPersonThomas();

            // Act
            var dump = ObjectDumperCSharp.Dump(person);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var person = new Person\n\r{\n\r  Name = \"Thomas\",\n\r  Char = '',\n\r  Age = 30,\n\r  GetOnly = 11,\n\r  Bool = false,\n\r  Byte = 0,\n\r  ByteArray = new Byte[]\n\r  {\n\r    1,\n\r    2,\n\r    3,\n\r    4\n\r  },\n\r  SByte = 0,\n\r  Float = 0f,\n\r  Uint = 0,\n\r  Long = 0L,\n\r  ULong = 0L,\n\r  Short = 0,\n\r  UShort = 0,\n\r  Decimal = 0m,\n\r  Double = 0d,\n\r  DateTime = DateTime.MinValue,\n\r  NullableDateTime = null,\n\r  Enum = System.DateTimeKind.Unspecified\n\r};");
        }

        [Fact]
        public void ShouldDumpObject_WithDumpOptions()
        {
            // Arrange
            var person = PersonFactory.GetPersonThomas();
            var options = new DumpOptions
            {
                IndentSize = 1,
                IndentChar = '\t',
                LineBreakChar = "\n",
                SetPropertiesOnly = true
            };

            // Act
            var dump = ObjectDumperCSharp.Dump(person, options);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var person = new Person\n{\n	Name = \"Thomas\",\n	Char = '',\n	Age = 30,\n	Bool = false,\n	Byte = 0,\n	ByteArray = new Byte[]\n	{\n		1,\n		2,\n		3,\n		4\n	},\n	SByte = 0,\n	Float = 0f,\n	Uint = 0,\n	Long = 0L,\n	ULong = 0L,\n	Short = 0,\n	UShort = 0,\n	Decimal = 0m,\n	Double = 0d,\n	DateTime = DateTime.MinValue,\n	NullableDateTime = null,\n	Enum = System.DateTimeKind.Unspecified\n};");
        }

        [Fact]
        public void ShouldDumpEnumerable()
        {
            // Arrange
            var persons = PersonFactory.GeneratePersons(count: 2).ToList();

            // Act
            var dump = ObjectDumperCSharp.Dump(persons);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var listPerson = new List<Person>\n\r{\n\r  new Person\n\r  {\n\r    Name = \"Person 1\",\n\r    Char = '',\n\r    Age = 3,\n\r    GetOnly = 11,\n\r    Bool = false,\n\r    Byte = 0,\n\r    ByteArray = new Byte[]\n\r    {\n\r      1,\n\r      2,\n\r      3,\n\r      4\n\r    },\n\r    SByte = 0,\n\r    Float = 0f,\n\r    Uint = 0,\n\r    Long = 0L,\n\r    ULong = 0L,\n\r    Short = 0,\n\r    UShort = 0,\n\r    Decimal = 0m,\n\r    Double = 0d,\n\r    DateTime = DateTime.MinValue,\n\r    NullableDateTime = null,\n\r    Enum = System.DateTimeKind.Unspecified\n\r  },\n\r  new Person\n\r  {\n\r    Name = \"Person 2\",\n\r    Char = '',\n\r    Age = 3,\n\r    GetOnly = 11,\n\r    Bool = false,\n\r    Byte = 0,\n\r    ByteArray = new Byte[]\n\r    {\n\r      1,\n\r      2,\n\r      3,\n\r      4\n\r    },\n\r    SByte = 0,\n\r    Float = 0f,\n\r    Uint = 0,\n\r    Long = 0L,\n\r    ULong = 0L,\n\r    Short = 0,\n\r    UShort = 0,\n\r    Decimal = 0m,\n\r    Double = 0d,\n\r    DateTime = DateTime.MinValue,\n\r    NullableDateTime = null,\n\r    Enum = System.DateTimeKind.Unspecified\n\r  }\n\r};");
        }

        [Fact]
        public void ShouldDumpNestedObjects()
        {
            // Arrange
            var persons = PersonFactory.GeneratePersons(count: 2).ToList();
            var organization = new Organization { Name = "superdev gmbh", Persons = persons };

            // Act
            var dump = ObjectDumperCSharp.Dump(organization);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var organization = new Organization\n\r{\n\r  Name = \"superdev gmbh\",\n\r  Persons = new List<Person>\n\r  {\n\r    new Person\n\r    {\n\r      Name = \"Person 1\",\n\r      Char = '',\n\r      Age = 3,\n\r      GetOnly = 11,\n\r      Bool = false,\n\r      Byte = 0,\n\r      ByteArray = new Byte[]\n\r      {\n\r        1,\n\r        2,\n\r        3,\n\r        4\n\r      },\n\r      SByte = 0,\n\r      Float = 0f,\n\r      Uint = 0,\n\r      Long = 0L,\n\r      ULong = 0L,\n\r      Short = 0,\n\r      UShort = 0,\n\r      Decimal = 0m,\n\r      Double = 0d,\n\r      DateTime = DateTime.MinValue,\n\r      NullableDateTime = null,\n\r      Enum = System.DateTimeKind.Unspecified\n\r    },\n\r    new Person\n\r    {\n\r      Name = \"Person 2\",\n\r      Char = '',\n\r      Age = 3,\n\r      GetOnly = 11,\n\r      Bool = false,\n\r      Byte = 0,\n\r      ByteArray = new Byte[]\n\r      {\n\r        1,\n\r        2,\n\r        3,\n\r        4\n\r      },\n\r      SByte = 0,\n\r      Float = 0f,\n\r      Uint = 0,\n\r      Long = 0L,\n\r      ULong = 0L,\n\r      Short = 0,\n\r      UShort = 0,\n\r      Decimal = 0m,\n\r      Double = 0d,\n\r      DateTime = DateTime.MinValue,\n\r      NullableDateTime = null,\n\r      Enum = System.DateTimeKind.Unspecified\n\r    }\n\r  }\n\r};");
        }

        [Fact]
        public void ShouldDumpMultipleGenericTypes()
        {
            // Arrange
            var person = PersonFactory.GeneratePersons(count: 1).First();
            var genericClass = new GenericClass<string, float, Person> { Prop1 = "Test", Prop2 = 123.45f, Prop3 = person };

            // Act
            var dump = ObjectDumperCSharp.Dump(genericClass);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var genericClass = new GenericClass<String, Single, Person>\n\r{\n\r  Prop1 = \"Test\",\n\r  Prop2 = 123.45f,\n\r  Prop3 = new Person\n\r  {\n\r    Name = \"Person 1\",\n\r    Char = '',\n\r    Age = 2,\n\r    GetOnly = 11,\n\r    Bool = false,\n\r    Byte = 0,\n\r    ByteArray = new Byte[]\n\r    {\n\r      1,\n\r      2,\n\r      3,\n\r      4\n\r    },\n\r    SByte = 0,\n\r    Float = 0f,\n\r    Uint = 0,\n\r    Long = 0L,\n\r    ULong = 0L,\n\r    Short = 0,\n\r    UShort = 0,\n\r    Decimal = 0m,\n\r    Double = 0d,\n\r    DateTime = DateTime.MinValue,\n\r    NullableDateTime = null,\n\r    Enum = System.DateTimeKind.Unspecified\n\r  }\n\r};");
        }

        [Fact]
        public void ShouldDumpObjectWithMaxLevel()
        {
            // Arrange
            var persons = PersonFactory.GeneratePersons(count: 2).ToList();
            var organization = new Organization { Name = "superdev gmbh", Persons = persons };
            var options = new DumpOptions { MaxLevel = 1 };

            // Act
            var dump = ObjectDumperCSharp.Dump(organization, options);

            // Assert
            this.testOutputHelper.WriteLine(dump);

            dump.Should().NotBeNull();
            dump.Should().Be("var organization = new Organization\n\r{\n\r  Name = \"superdev gmbh\",\n\r  Persons = new List<Person>\n\r  {\n\r  }\n\r};");
        }

        [Fact]
        public void ShouldExcludeProperties()
        {
            // Arrange
            var testObject = new TestObject();
            var options = new DumpOptions { ExcludeProperties = { "Id", "NonExistent" } };

            // Act
            var dump = ObjectDumperCSharp.Dump(testObject, options);

            // Assert
            this.testOutputHelper.WriteLine(dump);

            dump.Should().NotBeNull();
            dump.Should().Be("var testObject = new TestObject\n\r{\n\r  NullableDateTime = null\n\r};");
        }


        [Fact]
        public void ShouldOrderProperties()
        {
            // Arrange
            var testObject = new OrderPropertyTestObject();
            var options = new DumpOptions { PropertyOrderBy = p => p.Name };

            // Act
            var dump = ObjectDumperCSharp.Dump(testObject, options);

            // Assert
            this.testOutputHelper.WriteLine(dump);

            dump.Should().NotBeNull();
            dump.Should().Be("var orderPropertyTestObject = new OrderPropertyTestObject\n\r{\n\r  A = null,\n\r  B = null,\n\r  C = null\n\r};");
        }

        [Fact]
        public void ShouldDumpDateTime_DateTimeKind_Unspecified()
        {
            // Arrange
            var dateTime = new DateTime(2000, 01, 01, 23, 59, 59, DateTimeKind.Unspecified);

            // Act
            var dump = ObjectDumperCSharp.Dump(dateTime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dateTime = DateTime.ParseExact(\"2000-01-01T23:59:59.0000000\", \"O\", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);");


            var returnedDateTime = DateTime.ParseExact("2000-01-01T23:59:59.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            returnedDateTime.Should().Be(dateTime);
        }

        [Fact]
        public void ShouldDumpDateTime_DateTimeKind_Utc()
        {
            // Arrange
            var dateTime = new DateTime(2000, 01, 01, 23, 59, 59, DateTimeKind.Utc);

            // Act
            var dump = ObjectDumperCSharp.Dump(dateTime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dateTime = DateTime.ParseExact(\"2000-01-01T23:59:59.0000000Z\", \"O\", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);");


            var returnedDateTime = DateTime.ParseExact("2000-01-01T23:59:59.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            returnedDateTime.Should().Be(dateTime);
        }

        private static string GetUtcOffsetString()
        {
            var utcOffset = TimeZoneInfo.Local.BaseUtcOffset;
            return $"{(utcOffset >= TimeSpan.Zero ? "+" : "-")}{utcOffset:hh\\:mm}";
        }

        [Fact]
        public void ShouldDumpDateTime_DateTimeKind_Local()
        {
            // Arrange
            var dateTime = new DateTime(2000, 01, 01, 23, 59, 59, DateTimeKind.Local);

            // Act
            var dump = ObjectDumperCSharp.Dump(dateTime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be($"var dateTime = DateTime.ParseExact(\"2000-01-01T23:59:59.0000000{GetUtcOffsetString()}\", \"O\", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);");

            var returnedDateTime = DateTime.ParseExact($"2000-01-01T23:59:59.0000000{GetUtcOffsetString()}", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            returnedDateTime.Should().Be(dateTime);
        }

        [Fact]
        public void ShouldDumpDateTimeMinValue()
        {
            // Arrange
            var datetime = DateTime.MinValue;

            // Act
            var dump = ObjectDumperCSharp.Dump(datetime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dateTime = DateTime.MinValue;");
        }

        [Fact]
        public void ShouldDumpDateTimeMaxValue()
        {
            // Arrange
            var datetime = DateTime.MaxValue;

            // Act
            var dump = ObjectDumperCSharp.Dump(datetime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dateTime = DateTime.MaxValue;");
        }

        [Fact]
        public void ShouldDumpNullableObject()
        {
            // Arrange
            DateTime? datetime = null;

            // Act
            var dump = ObjectDumperCSharp.Dump(datetime);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var x = null;");
        }

        [Fact]
        public void ShouldDumpEnum()
        {
            // Arrange
            var dateTimeKind = DateTimeKind.Utc;

            // Act
            var dump = ObjectDumperCSharp.Dump(dateTimeKind);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dateTimeKind = System.DateTimeKind.Utc;");
        }



        [Fact]
        public void ShouldDumpDictionary()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>
            {
                { 1, "Value1" },
                { 2, "Value2" },
                { 3, "Value3" }
            };

            // Act
            var dump = ObjectDumperCSharp.Dump(dictionary);

            // Assert
            this.testOutputHelper.WriteLine(dump);
            dump.Should().NotBeNull();
            dump.Should().Be("var dictionaryInt32String = new Dictionary<Int32, String>\n\r{\n\r  { 1, \"Value1\" },\n\r  { 2, \"Value2\" },\n\r  { 3, \"Value3\" }\n\r};");
        }
    }

    public class CultureSpecificFixture2
    {
    }
}
