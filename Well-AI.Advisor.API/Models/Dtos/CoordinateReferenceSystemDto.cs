   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Dtos
{
	

	public class NameCRSDto {
		[Key]
		[Required]
		public string Code { get; set; }
		public string NamingSystem { get; set; }
		
		public string Text { get; set; }
	}

	public class GmlGeodeticCRSDto
	{
		[Key]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public string Scope { get; set; }
		public EllipsoidalCSDto EllipsoidalCS { get; set; }
		public UsesGeodeticDatumDto UsesGeodeticDatum { get; set; }

		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Gml { get; set; }
	}

	public class CoordinateReferenceSystemGeodeticCRSDto
	{
		[Key]
		public int CoRefGeodeticCRSId { get; set; }
		public NameCRSDto NameCRS { get; set; }
		public GmlGeodeticCRSDto GmlGeodeticCRS { get; set; }
	}

	public class CoordinateReferenceSystemDto {
		[Key]
		[Required]
		public string Uid { get; set; }
		public string Name { get; set; }
		public CoordinateReferenceSystemGeodeticCRSDto GeodeticCRS { get; set; }
		public ProjectedCRSDto ProjectedCRS { get; set; }
		public VerticalCRSDto VerticalCRS { get; set; }
		public string Name2 { get; set; }
	}

	public class ProjectedCRSDto {
		[Key]
		[Required]
		public int ProjectedCRSId { get; set; }
		public NameCRSDto NameCRS { get; set; }
		 
	}

	public class VerticalCRSDto {
		[Key]
		[Required]
		public int VerticalCRSId { get; set; }
		public NameCRSDto NameCRS { get; set; }
	}

	public class IdentifierDto {
		[Key]
		[Required]
		public string CodeSpace { get; set; }
		
		public string Text { get; set; }
	}

	public class NameDto {
		[Key]
		[Required]
		public string CodeSpace { get; set; }
		
		public string Text { get; set; }
	}

	public class AxisDirectionDto {
		[Key]
		[Required]
		public string CodeSpace { get; set; }
		
		public string Text { get; set; }
	}

	public class CoordinateSystemAxisDto {
		[Key]
		[Required]
		public string Id { get; set; }
		
		public IdentifierDto Identifier { get; set; }
		public string Name { get; set; }
		public string AxisAbbrev { get; set; }
		public AxisDirectionDto AxisDirection { get; set; }
		
		public string Uom { get; set; }
	}

	public class UsesAxisDto {
		[Key]
		[Required]
		public int UsesAxisId { get; set; }
		public CoordinateSystemAxisDto CoordinateSystemAxis { get; set; }
	}

	public class EllipsoidalCSDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public List<UsesAxisDto> UsesAxis { get; set; }
		
	}
 
	public class GreenwichLongitudeDto {
		[Key]
		[Required]
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class PrimeMeridianDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public GreenwichLongitudeDto GreenwichLongitude { get; set; }
		
	}

	public class UsesPrimeMeridianDto {
		[Key]
		[Required]
		public int UsesPrimeMeridianId { get; set; }
		public PrimeMeridianDto PrimeMeridian { get; set; }
	}

	public class SemiMajorAxisDto {
		[Key]
		[Required]
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class InverseFlatteningDto {
		[Key]
		[Required]
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SecondDefiningParameterDto {
		[Key]
		[Required]
		public int SecondDefiningParameterId { get; set; }
		public InverseFlatteningDto InverseFlattening { get; set; }
	}
 
	public class EllipsoidDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public string Description { get; set; }
		public IdentifierDto Identifier { get; set; }
		public List<NameDto> Name { get; set; }
		public SemiMajorAxisDto SemiMajorAxis { get; set; }
		public SecondDefiningParameterDto SecondDefiningParameter { get; set; }
		
	}

	public class UsesEllipsoidDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public EllipsoidDto Ellipsoid { get; set; }
	}

	public class GeodeticDatumDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public List<NameDto> Name { get; set; }
		public string Scope { get; set; }
		public string AnchorPoint { get; set; }
		public UsesPrimeMeridianDto UsesPrimeMeridian { get; set; }
		public UsesEllipsoidDto UsesEllipsoid { get; set; }
		
	}

	public class UsesGeodeticDatumDto {
		[Key]
		[Required]
		public int UsesGeodeticDatumId { get; set; }
		public GeodeticDatumDto GeodeticDatum { get; set; }
	}
 

	public class OperationParameterDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public string Description { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		
	}

	public class UsesParameterDto {
		[Key]
		[Required]
		public int UsesParameterId { get; set; }
		public OperationParameterDto OperationParameter { get; set; }
	}

	public class OperationMethodDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public string MethodFormula { get; set; }
		public string SourceDimensions { get; set; }
		public string TargetDimensions { get; set; }
		public List<UsesParameterDto> UsesParameter { get; set; }
		
	}

	public class UsesMethodDto {
		[Key]
		[Required]
		public string UsesMethodId { get; set; }
		public OperationMethodDto OperationMethod { get; set; }
	}

	public class CoordinateReferenceSystemValueDto
	{
		[Key]
		[Required]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ValueOfParameterDto {
		[Key]
		[Required]
		public int ValueOfParameterId { get; set; }
		public string Title { get; set; }
		public string Href { get; set; }
		public string Xlink { get; set; }
	}

	public class ParameterValueDto {
		[Key]
		[Required]
		public int ValueOfParameterId { get; set; }
		public CoordinateReferenceSystemValueDto Value { get; set; }
		public ValueOfParameterDto ValueOfParameter { get; set; }
	}

	public class UsesValueDto {
		[Key]
		[Required]
		public int UsesValueId { get; set; }
		public ParameterValueDto ParameterValue { get; set; }
	}

	public class ConversionDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public string Scope { get; set; }
		public UsesMethodDto UsesMethod { get; set; }
		public List<UsesValueDto> UsesValue { get; set; }
		
	}

	public class DefinedByConversionDto {
		[Key]
		[Required]
		public string DefinedByConversionId { get; set; }
		public ConversionDto Conversion { get; set; }
	}

	public class UsesEllipsoidalCSDto {
		[Key]
		[Required]
		public string EllipsId { get; set; }
		public EllipsoidalCSDto EllipsoidalCS { get; set; }
	}

	public class GeographicCRSDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public string Scope { get; set; }
		public UsesEllipsoidalCSDto UsesEllipsoidalCS { get; set; }
		public UsesGeodeticDatumDto UsesGeodeticDatum { get; set; }
		
	}

	public class BaseGeographicCRSDto {
		[Key]
		[Required]
		public int BaseGeographicCRSId { get; set; }
		public GeographicCRSDto GeographicCRS { get; set; }
	}

	public class CartesianCSDto {
		[Key]
		[Required]
		public string Id { get; set; }
		public IdentifierDto Identifier { get; set; }
		public NameDto Name { get; set; }
		public List<UsesAxisDto> UsesAxis { get; set; }
		
	}

	public class UsesCartesianCSDto {
		[Key]
		[Required]
		public int Id { get; set; }
		public CartesianCSDto CartesianCS { get; set; }
	}
 

}
