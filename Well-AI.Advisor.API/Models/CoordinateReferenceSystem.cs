   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Models
{

	public class NameCRS
	{
		[Key]
		[Required]
		public string Code { get; set; }
		public string NamingSystem { get; set; }

		public string Text { get; set; }
	}

	public class CoordinateReferenceSystemGeodeticCRS
	{
		[Key]
		public int CoRefGeodeticCRSId { get; set; }
		public NameCRS NameCRS { get; set; }
		public GmlGeodeticCRS GmlGeodeticCRS { get; set; }
	}

	public class GmlGeodeticCRS
	{
		[Key]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public string Scope { get; set; }
		public EllipsoidalCS EllipsoidalCS { get; set; }
		public UsesGeodeticDatum UsesGeodeticDatum { get; set; }
		
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Gml { get; set; }
	}
	public class CoordinateReferenceSystem
	{
		[Key]
		[Required]
		public string Uid { get; set; }
		public string Name { get; set; }
		public CoordinateReferenceSystemGeodeticCRS GeodeticCRS { get; set; }
		public ProjectedCRS ProjectedCRS { get; set; }
		public VerticalCRS VerticalCRS { get; set; }
		public string Name2 { get; set; }
	}

	public class ProjectedCRS
	{
		[Key]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public string Scope { get; set; }
		public DefinedByConversion DefinedByConversion { get; set; }
		public BaseGeographicCRS BaseGeographicCRS { get; set; }
		public UsesCartesianCS UsesCartesianCS { get; set; }
		
		public string SchemaLocation { get; set; }
		public string Gml { get; set; }
	}

	public class VerticalCRS
	{
		[Key]
		[Required]
		public int VerticalCRSId { get; set; }
		public NameCRS NameCRS { get; set; }
	}

	public class Identifier
	{
		[Key]
		[Required]
		public string CodeSpace { get; set; }

		public string Text { get; set; }
	}

	public class Name
	{
		[Key]
		[Required]
		public string CodeSpace { get; set; }

		public string Text { get; set; }
	}

	public class AxisDirection
	{
		[Key]
		[Required]
		public string CodeSpace { get; set; }

		public string Text { get; set; }
	}

	public class CoordinateSystemAxis
	{
		[Key]
		[Required]
		public string Id { get; set; }

		public Identifier Identifier { get; set; }
		public string Name { get; set; }
		public string AxisAbbrev { get; set; }
		public AxisDirection AxisDirection { get; set; }

		public string Uom { get; set; }
	}

	public class UsesAxis
	{
		[Key]
		[Required]
		public int UsesAxisId { get; set; }
		public CoordinateSystemAxis CoordinateSystemAxis { get; set; }
	}

	public class EllipsoidalCS
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public List<UsesAxis> UsesAxis { get; set; }

	}
 

	public class GreenwichLongitude
	{
		[Key]
		[Required]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class PrimeMeridian
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public GreenwichLongitude GreenwichLongitude { get; set; }

	}

	public class UsesPrimeMeridian
	{
		[Key]
		[Required]
		public int UsesPrimeMeridianId { get; set; }
		public PrimeMeridian PrimeMeridian { get; set; }
	}

	public class SemiMajorAxis
	{
		[Key]
		[Required]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class InverseFlattening
	{
		[Key]
		[Required]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SecondDefiningParameter
	{
		[Key]
		[Required]
		public int SecondDefiningParameterId { get; set; }
		public InverseFlattening InverseFlattening { get; set; }
	}
 

	public class Ellipsoid
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public string Description { get; set; }
		public Identifier Identifier { get; set; }
		public List<Name> Name { get; set; }
		public SemiMajorAxis SemiMajorAxis { get; set; }
		public SecondDefiningParameter SecondDefiningParameter { get; set; }

	}

	public class UsesEllipsoid
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Ellipsoid Ellipsoid { get; set; }
	}

	public class GeodeticDatum
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public List<Name> Name { get; set; }
		public string Scope { get; set; }
		public string AnchorPoint { get; set; }
		public UsesPrimeMeridian UsesPrimeMeridian { get; set; }
		public UsesEllipsoid UsesEllipsoid { get; set; }

	}

	public class UsesGeodeticDatum
	{
		[Key]
		[Required]
		public int UsesGeodeticDatumId { get; set; }
		public GeodeticDatum GeodeticDatum { get; set; }
	}

 

	public class OperationParameter
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public string Description { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }

	}

	public class UsesParameter
	{
		[Key]
		[Required]
		public int UsesParameterId { get; set; }
		public OperationParameter OperationParameter { get; set; }
	}

	public class OperationMethod
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public string MethodFormula { get; set; }
		public string SourceDimensions { get; set; }
		public string TargetDimensions { get; set; }
		public List<UsesParameter> UsesParameter { get; set; }

	}

	public class UsesMethod
	{
		[Key]
		[Required]
		public string UsesMethodId { get; set; }
		public OperationMethod OperationMethod { get; set; }
	}
 

	public class ValueOfParameter
	{
		[Key]
		[Required]
		public int ValueOfParameterId { get; set; }
		public string Title { get; set; }
		public string Href { get; set; }
		public string Xlink { get; set; }
	}

	public class CoordinateReferenceSystemValue
	{
		[Key]
		[Required]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ParameterValue
	{
		[Key]
		[Required]
		public int ValueOfParameterId { get; set; }
		public CoordinateReferenceSystemValue Value { get; set; }
		public ValueOfParameter ValueOfParameter { get; set; }
	}

	public class UsesValue
	{
		[Key]
		[Required]
		public int UsesValueId { get; set; }
		public ParameterValue ParameterValue { get; set; }
	}

	public class Conversion
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public string Scope { get; set; }
		public UsesMethod UsesMethod { get; set; }
		public List<UsesValue> UsesValue { get; set; }

	}

	public class DefinedByConversion
	{
		[Key]
		[Required]
		public string DefinedByConversionId { get; set; }
		public Conversion Conversion { get; set; }
	}

	public class UsesEllipsoidalCS
	{
		[Key]
		[Required]
		public string EllipsId { get; set; }
		public EllipsoidalCS EllipsoidalCS { get; set; }
	}

	public class GeographicCRS
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public string Scope { get; set; }
		public UsesEllipsoidalCS UsesEllipsoidalCS { get; set; }
		public UsesGeodeticDatum UsesGeodeticDatum { get; set; }

	}

	public class BaseGeographicCRS
	{
		[Key]
		[Required]
		public int BaseGeographicCRSId { get; set; }
		public GeographicCRS GeographicCRS { get; set; }
	}

	public class CartesianCS
	{
		[Key]
		[Required]
		public string Id { get; set; }
		public Identifier Identifier { get; set; }
		public Name Name { get; set; }
		public List<UsesAxis> UsesAxis { get; set; }

	}

	public class UsesCartesianCS
	{
		[Key]
		[Required]
		public int Id { get; set; }
		public CartesianCS CartesianCS { get; set; }
	}
  
}
