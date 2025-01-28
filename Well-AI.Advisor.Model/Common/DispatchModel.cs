using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class DispatchModel
    {

    }

    public class location
    {
        public string? user_id { get; set; }
        public DateTime logged_dt { get; set; }

        public double? latitude { get; set; }
        public double? longitude { get; set; }

        public string? activity_id { get;set; }

        public activity? activity { get; set; }

        public string? message { get; set; }

        public string? details { get; set; }

        public string? time_remaining { get; set; }
        public string? distance_remaining { get; set; }

        public DateTime? eta_timestamp_UT { get; set; }
        public string? eta_timestamp { get; set; }
    }

    public class UserCurrentLocation
    {
   
        public string? result { get; set; }
   
        public location? location { get; set; }
      
        public string? message { get; set; }
      
        public string? user_key { get; set; }
    }

    public class UserCurrentLocationdetails
    {

        public string? result { get; set; }

        public location? location { get; set; }

        public string? message { get; set; }

        public string? user_key { get; set; }

        public string? username { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? area { get; set; }
    }

    public class UsersRoutesdetails
    {    //  [JsonProperty("result")]
        public string result { get; set; }
    //   [JsonProperty("location")]
       public locationdetails location { get; set; }
   //   [JsonProperty("message")]
    public string? message { get; set; }
  //     [JsonProperty("user_key")]
        public string user_key { get; set; }
    }
  

        public class locationdetails
    {
        public string user_id { get; set; }
        public string logged_dt { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }

        public string activity_id { get; set; }

        //public activity? activity { get; set; }

        //public string? message { get; set; }

        //public string? details { get; set; }

        //public string? time_remaining { get; set; }
        //public string? distance_remaining { get; set; }

         public DateTime? eta_timestamp_UT { get; set; }
        //public string? eta_timestamp { get; set; }
    }

    public class activity
    {
        public string? origin { get; set; }

        public string? destination { get; set; }
        public string? well_id { get; set; }
        public string? rig_id { get; set; }
    }

    public class UserRoutes
    {
        [JsonProperty("result")]
        public string result { get; set; }
        [JsonProperty("location")]
        public location location { get; set; }
        [JsonProperty("destinations")]
        public destinations[] destinations { get; set; }
        [JsonProperty("dispatch_instructions")]
        public string dispatch_instructions { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }

    public class destinations
    {
        public int activity_id { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }

        public string well_id { get; set; }
        public string rig_id { get; set; }

        public directions directions { get; set; }
        public destination_coordinates destination_coordinates { get; set; }
        public user_path[] user_path { get; set; }

        public DateTime? eta_timestamp_UT { get; set; }
    }

    public class directions
    {
        public legs[] legs { get; set; }

    }

    public class destination_coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class legs
    {
        public paths[] path { get; set; }
        public double duration { get; set; }
        public double distance { get; set; }
        public double distance_miles { get; set; }

        public string source { get; set; }

    }

    public class paths
    {
        public double lat { get; set; }
        public double lng { get; set; }

        public long point_id { get; set; }
        public double speed { get; set; }
        public string summary { get; set; }

        public intersection intersection { get; set; }

        public maneuver maneuver { get; set; }

    }
    public class intersection
    {
        public string[] classes { get; set; }
        public string[] bearings { get; set; }
        public string[] entry { get; set; }
        public mapbox_streets_v8 mapbox_streets_v8 { get; set; }
        public bool? is_urban { get; set; }
        public int? admin_index { get; set; }

        [Display(Name = "out", Description = "")]
        public int? @out  { get; set; }
        [Display(Name = "in", Description = "")]
        public int? @in { get; set; }
        public int? geometry_index { get; set; }
        
        public double? turn_weight { get; set; }
        public double? duration { get; set; }
        public double? turn_duration { get; set; }
    }
    public class mapbox_streets_v8
    {
        [Display(Name = "class", Description = "")]
        public string classname { get;set;}
    }
    public class maneuver
    {
        public string type { get; set; }
        public string instruction { get; set; }
        public int? bearing_after { get; set; }
        public mapbox_streets_v8 mapbox_streets_v8 { get; set; }
        public int? bearing_before { get; set; }

    }

    public class userdestinations
    {
        public string type { get; set; }
        public features[] features { get; set; }
    }
    public class features {
        public string type { get; set; }
        public geometry geometry { get; set; }
        public properties properties { get; set; }
    }
    public class geometry
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }
    public class properties
    {
        public string title { get; set; }
        public string description { get; set; }
    }
    public class user_path
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public double speed { get; set; }
        public string heading { get; set; }
        public double timestamp { get; set; }
    }

    public class destinations_array
    {
        public string type { get; set; }
        public string id { get; set; }

    }
    public class destinationssimulationrequest
    {
        public string origin { get; set; }
        public string priority { get; set; }
        public List<destinations_array> destinationsarray{ get;set; }
    }

}

