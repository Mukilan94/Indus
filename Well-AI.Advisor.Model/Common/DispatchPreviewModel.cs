using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class DispatchPreviewModel
    {
    }
	public class MapSimulationObject
	{
		[JsonProperty("colors")]
		public List<string> Colors { get; set; }
		[JsonProperty("destinations")]
		public List<string> destinations { get; set; }
		[JsonProperty("directions")]
		public List<DirectionPreview> directions { get; set; }
		[JsonProperty("totals")]
		public Totals totals { get; set; }
		[JsonProperty("bounds")]
		public Dictionary<string, List<double>> bounds { get; set; }
		[JsonProperty("multiparams")]
		public Multiparams multiparams { get; set; }
	}
	public class DirectionPreview
	{
		[JsonProperty("origin")]
		public Origin origin { get; set; }
		[JsonProperty("destination")]
		public DestinationPreview destination { get; set; }
		[JsonProperty("mode")]
		public string mode { get; set; }
		[JsonProperty("legs")]
		public List<Leg> legs { get; set; }
		[JsonProperty("total_duration")]
		public double? total_duration { get; set; }
		[JsonProperty("total_distance")]
		public double? total_distance { get; set; }
		[JsonProperty("total_duration_display")]
		public string total_duration_display { get; set; }
		[JsonProperty("total_distance_display")]
		public string total_distance_display { get; set; }
		[JsonProperty("color")]
		public string color { get; set; }
		[JsonProperty("sources")]
		public List<string> sources { get; set; }
		[JsonProperty("sources_display")]
		public string sources_display { get; set; }
	}
	public class Origin
	{
		[JsonProperty("latitude")]
		public double? latitude { get; set; }
		[JsonProperty("longitude")]
		public double? longitude { get; set; }
		[JsonProperty("place")]
		public string place { get; set; }
		[JsonProperty("external")]
		public bool? external { get; set; }
		[JsonProperty("name")]
		public string name { get; set; }
	}
	public class DestinationPreview
	{
		[JsonProperty("latitude")]
		public string latitude { get; set; }
		[JsonProperty("longitude")]
		public string longitude { get; set; }
		[JsonProperty("place")]
		public string place { get; set; }
		[JsonProperty("internal")]
		public bool? Internal { get; set; }
		[JsonProperty("well")]
		public Well well { get; set; }
		[JsonProperty("point_id")]
		public string point_id { get; set; }
		[JsonProperty("cluster")]
		public string cluster { get; set; }
		[JsonProperty("mandatory")]
		public object mandatory { get; set; }
	}
	public class Well
	{
		[JsonProperty("id")]
		public string id { get; set; }
		[JsonProperty("latitude")]
		public string latitude { get; set; }
		[JsonProperty("longitude")]
		public string longitude { get; set; }
		[JsonProperty("name")]
		public string name { get; set; }
		[JsonProperty("nearest_point")]
		public string nearest_point { get; set; }
		[JsonProperty("staging")]
		public string staging { get; set; }
	}
	public class Leg
	{
		[JsonProperty("path")]
		public List<PathPreview> path { get; set; }
		[JsonProperty("duration")]
		public double? duration { get; set; }
		[JsonProperty("distance")]
		public double? distance { get; set; }
		[JsonProperty("distance_miles")]
		public double? distance_miles { get; set; }
		[JsonProperty("source")]
		public string source { get; set; }
	}
	public class PathPreview
	{
		[JsonProperty("lat")]
		public double? lat { get; set; }
		[JsonProperty("lng")]
		public double? lng { get; set; }
		[JsonProperty("speed")]
		public double? speed { get; set; }
		[JsonProperty("intersection")]
		public Intersection intersection { get; set; }
	}
	public class Intersection
	{
		[JsonProperty("lanes")]
		public List<Lane> lanes { get; set; }
		[JsonProperty("mapbox_streets_v8")]
		public MapboxStreetsV8 mapbox_streets_v8 { get; set; }
		[JsonProperty("geometry_index")]
		public int? geometry_index { get; set; }
		[JsonProperty("admin_index")]
		public int? admin_index { get; set; }
		[JsonProperty("weight")]
		public double? weight { get; set; }
		[JsonProperty("is_urban")]
		public bool? is_urban { get; set; }
		[JsonProperty("traffic_signal")]
		public bool? traffic_signal { get; set; }
		[JsonProperty("turn_duration")]
		public double? turn_duration { get; set; }
		[JsonProperty("turn_weight")]
		public double? turn_weight { get; set; }
		[JsonProperty("duration")]
		public double? duration { get; set; }
		[JsonProperty("bearings")]
		public List<int> bearings { get; set; }
		[JsonProperty("out")]
		public int? Out { get; set; }
		[JsonProperty("in")]
		public int? In { get; set; }
		[JsonProperty("entry")]
		public List<bool> Entry { get; set; }
	}
	public class Lane
	{
		[JsonProperty("indications")]
		public List<string> indications { get; set; }
		[JsonProperty("valid_indication")]
		public string valid_indication { get; set; }
		[JsonProperty("valid")]
		public bool? valid { get; set; }
		[JsonProperty("active")]
		public bool? active { get; set; }
	}
	public class MapboxStreetsV8
	{
		[JsonProperty("class")]
		public string Class { get; set; }
	}
	public class Totals
	{
		[JsonProperty("distance")]
		public double? distance { get; set; }
		[JsonProperty("duration")]
		public double? duration { get; set; }
		[JsonProperty("distance_display")]
		public string distance_display { get; set; }
		[JsonProperty("duration_display")]
		public string duration_display { get; set; }
	}
	public class Multiparams
	{
		[JsonProperty("origin")]
		public string origin { get; set; }
		[JsonProperty("well_id")]
		public Dictionary<string, string> well_id { get; set; }
		[JsonProperty("rig_id")]
		public Dictionary<string, string> rig_id { get; set; }
		[JsonProperty("optimize")]
		public int? optimize { get; set; }
		[JsonProperty("priority")]
		public string priority { get; set; }
	}

}
