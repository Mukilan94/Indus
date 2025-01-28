using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{

    public class AuxInput1
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class AuxInput2
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class BatteryMilliVolts
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class EngineState
    {
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class CheckEngineLights
    {
        public bool emissionsIsOn { get; set; }
        public bool protectIsOn { get; set; }
        public bool stopIsOn { get; set; }
        public bool warningIsOn { get; set; }
    }

    public class DiagnosticTroubleCode
    {
        public string fmiDescription { get; set; }
        public int fmiId { get; set; }
        public int milStatus { get; set; }
        public int occurrenceCount { get; set; }
        public string sourceAddressName { get; set; }
        public string spnDescription { get; set; }
        public int spnId { get; set; }
        public int txId { get; set; }
    }

    public class J1939
    {
        public CheckEngineLights checkEngineLights { get; set; }
        public IList<DiagnosticTroubleCode> diagnosticTroubleCodes { get; set; }
    }

    public class ConfirmedDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class MonitorStatus
    {
        public string catalyst { get; set; }
        public string comprehensive { get; set; }
        public string egr { get; set; }
        public string evapSystem { get; set; }
        public string fuel { get; set; }
        public string heatedCatalyst { get; set; }
        public string heatedO2Sensor { get; set; }
        public string isoSaeReserved { get; set; }
        public string misfire { get; set; }
        public int notReadyCount { get; set; }
        public string o2Sensor { get; set; }
        public string secondaryAir { get; set; }
    }

    public class PendingDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class PermanentDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class ObdiiDiagnosticTroubleCode
    {
        public IList<ConfirmedDtc> confirmedDtcs { get; set; }
        public string ignitionType { get; set; }
        public bool milStatus { get; set; }
        public MonitorStatus monitorStatus { get; set; }
        public IList<PendingDtc> pendingDtcs { get; set; }
        public IList<PermanentDtc> permanentDtcs { get; set; }
        public int txId { get; set; }
    }

    public class Obdii
    {
        public bool checkEngineLightIsOn { get; set; }
        public IList<ObdiiDiagnosticTroubleCode> diagnosticTroubleCodes { get; set; }
    }

    public class FaultCodes
    {
        public string canBusType { get; set; }
        public J1939 j1939 { get; set; }
        public Obdii obdii { get; set; }
        public DateTime time { get; set; }
    }

    public class FuelPercent
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class GpsDistanceMeters
    {
        public DateTime time { get; set; }
        public double value { get; set; }
    }

    public class GpsOdometerMeters
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class ObdEngineSeconds
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class ObdOdometerMeters
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechicleStatusDatum
    {
        public AuxInput1 auxInput1 { get; set; }
        public AuxInput2 auxInput2 { get; set; }
        public BatteryMilliVolts batteryMilliVolts { get; set; }
        public EngineState engineState { get; set; }
        public FaultCodes faultCodes { get; set; }
        public FuelPercent fuelPercent { get; set; }
        public GpsDistanceMeters gpsDistanceMeters { get; set; }
        public GpsOdometerMeters gpsOdometerMeters { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public ObdEngineSeconds obdEngineSeconds { get; set; }
        public ObdOdometerMeters obdOdometerMeters { get; set; }
    }

   
    public class VechicleStatusModel
    {
        public IList<VechicleStatusDatum> data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class VechicleStatusResponse
    {
        public List<VechicleStatusModel> Response { get; set; }


    }
}
