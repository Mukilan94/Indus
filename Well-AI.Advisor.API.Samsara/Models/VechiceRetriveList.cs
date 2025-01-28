using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{
    

    public class VechiceRetriveAuxInput1
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class VechiceRetriveAuxInput2
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class VechiceRetriveBatteryMilliVolt
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechiceRetriveEngineState
    {
        public DateTime time { get; set; }
        public string value { get; set; }
    }

    public class VechiceRetriveCheckEngineLights
    {
        public bool emissionsIsOn { get; set; }
        public bool protectIsOn { get; set; }
        public bool stopIsOn { get; set; }
        public bool warningIsOn { get; set; }
    }

    public class VechiceRetriveDiagnosticTroubleCode
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

    public class VechiceRetriveJ1939
    {
        public CheckEngineLights checkEngineLights { get; set; }
        public IList<DiagnosticTroubleCode> diagnosticTroubleCodes { get; set; }
    }

    public class VechiceRetriveConfirmedDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class VechiceRetriveMonitorStatus
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

    public class VechiceRetrivePendingDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class VechiceRetrivePermanentDtc
    {
        public string dtcDescription { get; set; }
        public int dtcId { get; set; }
        public string dtcShortCode { get; set; }
    }

    public class ObdiiVechiceRetriveDiagnosticTroubleCode
    {
        public IList<VechiceRetriveConfirmedDtc> confirmedDtcs { get; set; }
        public string ignitionType { get; set; }
        public bool milStatus { get; set; }
        public VechiceRetriveMonitorStatus monitorStatus { get; set; }
        public IList<VechiceRetrivePendingDtc> pendingDtcs { get; set; }
        public IList<VechiceRetrivePermanentDtc> permanentDtcs { get; set; }
        public int txId { get; set; }
    }

    public class VechiceRetriveObdii
    {
        public bool checkEngineLightIsOn { get; set; }
        public IList<ObdiiVechiceRetriveDiagnosticTroubleCode> diagnosticTroubleCodes { get; set; }
    }

    public class VechiceRetriveFaultCode
    {
        public string canBusType { get; set; }
        public VechiceRetriveJ1939 j1939 { get; set; }
        public VechiceRetriveObdii obdii { get; set; }
        public DateTime time { get; set; }
    }

    public class VechiceRetriveFuelPercent
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechiceRetriveGpsDistanceMeter
    {
        public DateTime time { get; set; }
        public double value { get; set; }
    }

    public class VechiceRetriveGpsOdometerMeter
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechiceRetriveObdEngineSecond
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechiceRetriveObdOdometerMeter
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class VechiceRetriveDatum
    {
        public IList<VechiceRetriveAuxInput1> auxInput1 { get; set; }
        public IList<VechiceRetriveAuxInput2> auxInput2 { get; set; }
        public IList<VechiceRetriveBatteryMilliVolt> batteryMilliVolts { get; set; }
        public IList<VechiceRetriveEngineState> engineStates { get; set; }
        public IList<VechiceRetriveFaultCode> faultCodes { get; set; }
        public IList<VechiceRetriveFuelPercent> fuelPercents { get; set; }
        public IList<VechiceRetriveGpsDistanceMeter> gpsDistanceMeters { get; set; }
        public IList<VechiceRetriveGpsOdometerMeter> gpsOdometerMeters { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public IList<VechiceRetriveObdEngineSecond> obdEngineSeconds { get; set; }
        public IList<VechiceRetriveObdOdometerMeter> obdOdometerMeters { get; set; }
    }

    public class VechiceRetrivePagination
    {
        public string endCursor { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class VechiceRetriveList
    {
        public IList<VechiceRetriveDatum> data { get; set; }
        public Pagination pagination { get; set; }
    }

    class VechiceRetriveListResponse
    {
        public List<VechiceRetriveList> Response { get; set; }
    }

}
