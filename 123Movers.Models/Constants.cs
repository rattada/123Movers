using System.ComponentModel;

namespace _123Movers.Models
{
    public enum TermType : int
    {
        [Description("Recurring")]
        Recurring = 0,
        [Description("Non Recurring")]
        NonRecurring = 1,
        [Description("Recurring With Notice")]
        RecurringWithNotice = 2
    }

    public enum ServiceType : int
    {
        Local = 1009,
        Long = 1000,
        Both = 999
    }

    public static class Constants
    {
        public const string BASE_JS_FOLDER = "/Scripts/";
        public static readonly string VIEW = "~/Views/Shared/_CompanyInfo.cshtml";
        public static readonly string URL = "/Account/LogOff";
        public  static readonly string CurrentCompanyInfo = "CurrentCompanyInfo";

        public static readonly string NEW_BUDGET = "NEW";
        public static readonly string RENEWL_BUDGET = "RENEWAL INSERTION";
        public static readonly string EDIT_BUDGET = "EDIT";

        public static readonly int LOCAL = 1009;
        public static readonly int LONG = 1000;
        public static readonly int BOTH = 999;
        public static readonly string LOCAL_TEXT = "Local";
        public static readonly string LONG_TEXT = "Long";
        public static readonly string BOTH_TEXT = "Both";
        public static readonly string DEFAULT = "Default";

        public static readonly string Recurring = "0";
        public static readonly string NonRecurring = "1";
        public static readonly string RecurringWithNotice = "2";


        #region All  Stored ProceduresConstants

            #region Search Stored Procedures

            public static readonly string SP_COMPANY_SEARCH = "usp_CompanySearchv3";

            #endregion

            #region AreaCode Stored Procedures

            public static readonly string SP_GET_COMPANY_STATE_AREACODE_PRICE = "usp_GetCompanyStateAreacodePrice";
            public static readonly string SP_COMPANY_AREACODE_ADD = "usp_AddCompanyAreasCodes";
            public static readonly string SP_COMPANY_AREACODE_DELETE = "usp_DeleteCompanyAreasCodes";
            public static readonly string SP_ADD_COMPANY_PRICE_PERLEAD = "usp_AddCompanyPricePerLead";
            public static readonly string SP_GET_AVAILABLE_AREAS = "usp_availableAreas";

            #endregion

            #region Destination AreaCode Stored Procedures

            public static readonly string SP_GET_COMPANY_DEST_AREACODE = "usp_GetCompanyDestinationAreacode";

            #endregion


            #region Budget Stored Procedures

            public static readonly string SP_SAVE_BUDGET = "usp_SaveBudget";
            public static readonly string SP_GET_FILTER_RESULT = "usp_GetFiltersInfo";

            #endregion

            #region OriginZipCodes Stored Procedures

            public static readonly string SP_GET_COMPANY_SERVICE_AREACODES = "usp_GetCompanyServiceAreaCodes";
            public static readonly string SP_ADD_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_AddCompanyAreasOriginZipCodes";
            public static readonly string SP_DELETE_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_DeleteCompanyAreasOriginZipCodes";
            public static readonly string SP_GET_COMPANY_AVAILABLE_AREA_ORIGIN_ZIPCODES = "usp_GetCompanyAvailableAreasOrignZipCodes";
            public static readonly string SP_GET_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_GetCompanyAreasZipCodes";

            #endregion

            #region LeadLimit Stored Procedures
            public static readonly string SP_GET_COMPANY_LEADLIMIT = "usp_GetCompanyLeadLimit";
            public static readonly string SP_ADD_COMPANY_LEADLIMIT = "usp_AddCompanyLeadLimit";
            #endregion

            #region SpecificOriginDestAreaCodes Stored Procedures

            public static readonly string SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_ADD = "usp_AddCompanySpecificOriginDestinationAreacode";
            public static readonly string SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_DELETE = "usp_DeleteCompanySpecificOriginDestinationAreacode";
            public static readonly string SP_COMPANY_SPCFC_ORIGINDEST_AREACODE_GET = "usp_GetCompanySpecificOriginDestinationAreacode";
            public static readonly string SP_COMPANY_SPCFC_AVAIL_ORIGINDEST_AREACODE_GET = "usp_getCompanyStateAreacode";

            #endregion

            #region Specific States Stored Procedures

            public static readonly string SP_COMPANY_SPCFC_STATES_ADD = "usp_AddCompanySpecificOriginDestinationState";
            public static readonly string SP_COMPANY_SPCFC_STATES_DELETE = "usp_DeleteCompanySpecificOriginDestinationState";
            public static readonly string SP_COMPANY_SPCFC_STATES_GET = "usp_GetCompanySpecificOriginDestinationState";
            public static readonly string SP_COMPANY_AVAIL_STATES__GET = "usp_GetCompanyStates";

            #endregion

            #region Radius  Stored Procedures

            public static readonly string SP_COMPANY_ZIPCODES_BY_RADIUS_ADD = "usp_AddZipCodesByRadius";
            public static readonly string SP_COMPANY_ZIPCODES_BY_RADIUS_GET = "usp_GetZipCodesByRadius";

            #endregion


        #endregion
    }
}