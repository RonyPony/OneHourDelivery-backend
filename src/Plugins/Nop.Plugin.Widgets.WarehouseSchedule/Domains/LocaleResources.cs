using System.Collections.Generic;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Domains
{
    /// <summary>
    /// Local Resources of Plugin
    /// </summary>
    public static class LocaleResources
    {
        /// <summary>
        /// English Resources.
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Sunday"] = "Sunday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Monday"] = "Monday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Tuesday"] = "Tuesday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Wednesday"] = "Wednesday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Thursday"] = "Thursday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Friday"] = "Friday",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Saturday"] = "Saturday",

            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Schedule"] = "Schedule",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.IsActive"] = "Is Active",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.BeginHour"] = "Begin Hour",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.EndHour"] = "End Hour",

            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Error.BeginEndHourValidation"]= "Begin hour must be shorter than end hour."
        };


        /// <summary>
        /// Spanish Resources
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Sunday"] = "Domingo",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Monday"] = "Lunes",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Tuesday"] = "Martes",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Wednesday"] = "Miércoles",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Thursday"] = "Jueves",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Friday"] = "Viernes",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Saturday"] = "Sábado",

            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Schedule"] = "Horario",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.IsActive"] = "Está activo",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.BeginHour"] = "Hora de Inicio",
            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Table.Header.EndHour"] = "Hora de Finalización",

            [$"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Error.BeginEndHourValidation"] = "La hora de inicio debe ser menor a la hora de finalización."
        };
    }
}
