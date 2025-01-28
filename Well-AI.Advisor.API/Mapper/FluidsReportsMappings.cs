using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class FluidsReportsMappings : Profile
    {
        public FluidsReportsMappings()
        {

         CreateMap<FluidsReportMd, FluidsReportMdDto>().ReverseMap();
        CreateMap<FluidsReportTvd, FluidsReportTvdDto>().ReverseMap();
        CreateMap<FluidsReportDensity, FluidsReportDensityDto>().ReverseMap();
        CreateMap<FluidsReportVisFunnel, FluidsReportVisFunnelDto>().ReverseMap();
        CreateMap<FluidsReportTempVis, FluidsReportTempVisDto>().ReverseMap();
        CreateMap<FluidsReportPv, FluidsReportPvDto>().ReverseMap();
        CreateMap<FluidsReportYp, FluidsReportYpDto>().ReverseMap();
        CreateMap<FluidsReportGel10Sec, FluidsReportGel10SecDto>().ReverseMap();
        CreateMap<FluidsReportGel10Min, FluidsReportGel10MinDto>().ReverseMap();
        CreateMap<FluidsReportGel30Min, FluidsReportGel30MinDto>().ReverseMap();
        CreateMap<FluidsReportFilterCakeLtlp, FluidsReportFilterCakeLtlpDto>().ReverseMap();
        CreateMap<FluidsReportFiltrateLtlp, FluidsReportFiltrateLtlpDto>().ReverseMap();
        CreateMap<FluidsReportTempHthp, FluidsReportTempHthpDto>().ReverseMap();
        CreateMap<FluidsReportPresHthp, FluidsReportPresHthpDto>().ReverseMap();
        CreateMap<FluidsReportFiltrateHthp, FluidsReportFiltrateHthpDto>().ReverseMap();
        CreateMap<FluidsReportFilterCakeHthp, FluidsReportFilterCakeHthpDto>().ReverseMap();
        CreateMap<FluidsReportSolidsPc, FluidsReportSolidsPcDto>().ReverseMap();
        CreateMap<FluidsReportWaterPc, FluidsReportWaterPcDto>().ReverseMap();
        CreateMap<FluidsReportOilPc, FluidsReportOilPcDto>().ReverseMap();
        CreateMap<FluidsReportSandPc, FluidsReportSandPcDto>().ReverseMap();
        CreateMap<FluidsReportSolidsLowGravPc, FluidsReportSolidsLowGravPcDto>().ReverseMap();
        CreateMap<FluidsReportSolidsCalcPc, FluidsReportSolidsCalcPcDto>().ReverseMap();
        CreateMap<FluidsReportBaritePc, FluidsReportBaritePcDto>().ReverseMap();
        CreateMap<FluidsReportLcm, FluidsReportLcmDto>().ReverseMap();
        CreateMap<FluidsReportMbt, FluidsReportMbtDto>().ReverseMap();
        CreateMap<FluidsReportTempPh, FluidsReportTempPhDto>().ReverseMap();
        CreateMap<FluidsReportPm, FluidsReportPmDto>().ReverseMap();
        CreateMap<FluidsReportPmFiltrate, FluidsReportPmFiltrateDto>().ReverseMap();
        CreateMap<FluidsReportMf, FluidsReportMfDto>().ReverseMap();
        CreateMap<FluidsReportAlkalinityP1, FluidsReportAlkalinityP1Dto>().ReverseMap();
        CreateMap<FluidsReportAlkalinityP2, FluidsReportAlkalinityP2Dto>().ReverseMap();
        CreateMap<FluidsReportChloride, FluidsReportChlorideDto>().ReverseMap();
        CreateMap<FluidsReportCalcium, FluidsReportCalciumDto>().ReverseMap();
        CreateMap<FluidsReportMagnesium, FluidsReportMagnesiumDto>().ReverseMap();
        CreateMap<FluidsReportPotassium, FluidsReportPotassiumDto>().ReverseMap();
        CreateMap<FluidsReportTempRheom, FluidsReportTempRheomDto>().ReverseMap();
        CreateMap<FluidsReportPresRheom, FluidsReportPresRheomDto>().ReverseMap();
        CreateMap<FluidsReport, FluidsReportDto>().ReverseMap();
        CreateMap<FluidsReportRheometer, FluidsReportRheometerDto>().ReverseMap();
        CreateMap<FluidsReportBrinePc, FluidsReportBrinePcDto>().ReverseMap();
        CreateMap<FluidsReportLime, FluidsReportLimeDto>().ReverseMap();
        CreateMap<FluidsReportElectStab, FluidsReportElectStabDto>().ReverseMap();
        CreateMap<FluidsReportCalciumChloride, FluidsReportCalciumChlorideDto>().ReverseMap();
        CreateMap<FluidsReportSolidsHiGravPc, FluidsReportSolidsHiGravPcDto>().ReverseMap();
        CreateMap<FluidsReportPolymer, FluidsReportPolymerDto>().ReverseMap();
        CreateMap<FluidsReportSolCorPc, FluidsReportSolCorPcDto>().ReverseMap();
        CreateMap<FluidsReportOilCtg, FluidsReportOilCtgDto>().ReverseMap();
        CreateMap<FluidsReportSulfide, FluidsReportSulfideDto>().ReverseMap();
        CreateMap<FluidsReportFluid, FluidsReportFluidDto>().ReverseMap();
        CreateMap<FluidsReportCommonData, FluidsReportCommonDataDto>().ReverseMap();
        CreateMap<FluidsReportHardnessCa, FluidsReportHardnessCaDto>().ReverseMap();
 
        }

        
    }
}
