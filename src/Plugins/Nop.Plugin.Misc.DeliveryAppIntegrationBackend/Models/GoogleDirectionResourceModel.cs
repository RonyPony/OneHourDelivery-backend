using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents geocodedWaypoint.
    /// </summary>
    public sealed class GeocodedWaypoint
    {
        /// <summary>
        /// Get or ser geocoder status.
        /// </summary>
        public string geocoder_status { get; set; }

        /// <summary>
        /// Get or set place id.
        /// </summary>
        public string place_id { get; set; }

        /// <summary>
        /// Get or set types.
        /// </summary>
        public List<string> types { get; set; }
    }

    /// <summary>
    /// Represent northeast model.
    /// </summary>
    public sealed class Northeast
    {
        /// <summary>
        /// Get or set lat.
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// Get or set lng.
        /// </summary>
        public double lng { get; set; }
    }

    /// <summary>
    /// Represents southwest mode.
    /// </summary>
    public sealed class Southwest
    {
        /// <summary>
        /// Get or set lat.
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// Get or set lng.
        /// </summary>
        public double lng { get; set; }
    }

    /// <summary>
    /// Represents bounds model.
    /// </summary>
    public sealed class Bounds
    {
        /// <summary>
        /// Get or set northeast.
        /// </summary>
        public Northeast northeast { get; set; }

        /// <summary>
        /// Get or set southwest.
        /// </summary>
        public Southwest southwest { get; set; }
    }

    /// <summary>
    /// Represents distance model.
    /// </summary>
    public sealed class GoogleMapDistance
    {
        /// <summary>
        /// Get or set text.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Get or set value.
        /// </summary>
        public int value { get; set; }
    }

    /// <summary>
    /// Represents duration model.
    /// </summary>
    public sealed class Duration
    {
        /// <summary>
        /// Get or set text.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Get or set value.
        /// </summary>
        public int value { get; set; }
    }

    /// <summary>
    /// Represents endLocation model.
    /// </summary>
    public sealed class EndLocation
    {
        /// <summary>
        /// Get or set lat.
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// Get or set lng.
        /// </summary>
        public double lng { get; set; }
    }

    /// <summary>
    /// Represents startLocation model.
    /// </summary>
    public sealed class StartLocation
    {
        /// <summary>
        /// Get or set lat.
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// Get or set lng.
        /// </summary>
        public double lng { get; set; }
    }

    /// <summary>
    /// Represents polyline model.
    /// </summary>
    public sealed class Polyline
    {
        /// <summary>
        /// Get or set points.
        /// </summary>
        public string points { get; set; }
    }

    /// <summary>
    /// Represents step model.
    /// </summary>
    public sealed class Step
    {
        /// <summary>
        /// Get or set distance.
        /// </summary>
        public Distance distance { get; set; }

        /// <summary>
        /// Get or set duration.
        /// </summary>
        public Duration duration { get; set; }

        /// <summary>
        /// Get or set end_location.
        /// </summary>
        public EndLocation end_location { get; set; }

        /// <summary>
        /// Get or set htm_instructions.
        /// </summary>
        public string html_instructions { get; set; }

        /// <summary>
        /// Get or set polyline.
        /// </summary>
        public Polyline polyline { get; set; }

        /// <summary>
        /// Get or set start_location.
        /// </summary>
        public StartLocation start_location { get; set; }

        /// <summary>
        /// Get or set travel_mode.
        /// </summary>
        public string travel_mode { get; set; }

        /// <summary>
        /// Get or set maneuver.
        /// </summary>
        public string maneuver { get; set; }
    }

    /// <summary>
    /// Represents leg model.
    /// </summary>
    public sealed class Leg
    {
        /// <summary>
        /// Get or set distance.
        /// </summary>
        public Distance distance { get; set; }

        /// <summary>
        /// Get or set duration.
        /// </summary>
        public Duration duration { get; set; }

        /// <summary>
        /// Get or set end_address.
        /// </summary>
        public string end_address { get; set; }

        /// <summary>
        /// Get or set end_location.
        /// </summary>
        public EndLocation end_location { get; set; }

        /// <summary>
        /// Get or set start_address.
        /// </summary>
        public string start_address { get; set; }

        /// <summary>
        /// Get or set start_location.
        /// </summary>
        public StartLocation start_location { get; set; }

        /// <summary>
        /// Get or set steps.
        /// </summary>
        public List<Step> steps { get; set; }

        /// <summary>
        /// Get or set traffic_speed_entry.
        /// </summary>
        public List<object> traffic_speed_entry { get; set; }

        /// <summary>
        /// Get or set via_waypoint.
        /// </summary>
        public List<object> via_waypoint { get; set; }
    }

    /// <summary>
    /// Represents overviewPolyline  model.
    /// </summary>
    public sealed class OverviewPolyline
    {
        /// <summary>
        /// Get or set points.
        /// </summary>
        public string points { get; set; }
    }

    /// <summary>
    /// Represents route model.
    /// </summary>
    public sealed class Route
    {
        /// <summary>
        /// Get or set bounds.
        /// </summary>
        public Bounds bounds { get; set; }

        /// <summary>
        /// Get or set copyrights.
        /// </summary>
        public string copyrights { get; set; }

        /// <summary>
        /// Get or set legs.
        /// </summary>
        public List<Leg> legs { get; set; }

        /// <summary>
        /// Get or set overview_polyline.
        /// </summary>
        public OverviewPolyline overview_polyline { get; set; }

        /// <summary>
        /// Get or set summary.
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// Get or set warning.
        /// </summary>
        public List<object> warnings { get; set; }

        /// <summary>
        /// Get or set waypoint_order.
        /// </summary>
        public List<object> waypoint_order { get; set; }
    }

    /// <summary>
    /// Represents google direction resource model.
    /// </summary>
    public sealed class GoogleDirectionResourceModel
    {
        /// <summary>
        /// Get or set geocoded_waypoints.
        /// </summary>
        public List<GeocodedWaypoint> geocoded_waypoints { get; set; }

        /// <summary>
        /// Get or set routes.
        /// </summary>
        public List<Route> routes { get; set; }

        /// <summary>
        /// Get or set status.
        /// </summary>
        public string status { get; set; }
    }
}
