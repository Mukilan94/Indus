using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class DocumentManagerNewFolderModel
    {
        public string ParentPath { get; set; }
        [Required]
        public string NewFolderName { get; set; }
    }

    public class UploadsModel
    {
        public int sid { get; set; }
    }

    public class UploadsGridFileModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string WellName { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime Expire { get; set; }
        public bool IsActive { get; set; }
        public string VendorId { get; set; }
    }

    public class UploadFileWellModelCement
    {
        [Required(ErrorMessage ="Well is required")]
        public string WellIdCement { get; set; }
    }

    public class UploadFileWellModelPermits
    {
        [Required(ErrorMessage = "Well is required")]
        public string WellIdPermits { get; set; }
    }

    public class UploadFileWellModelDrill
    {
        [Required(ErrorMessage = "Well is required")]
        public string WellIdDrill { get; set; }
    }

    public class UploadFileWellModelMud
    {
        [Required(ErrorMessage = "Well is required")]
        public string WellIdMud { get; set; }
    }

    public class UploadFileVendorModel
    {
        [Required(ErrorMessage = "Vendor is required")]
        public string VendorId { get; set; }

        public bool ActiveStatus { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Expire Date is required")]
        public DateTime ExpireDate { get; set; }
    }

    public class UploadFileOperatorModel
    {
        [Required(ErrorMessage = "Operator is required")]
        public string OperatorId { get; set; }

        public bool ActiveStatus { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Expire Date is required")]
        public DateTime ExpireDate { get; set; }
    }
}
