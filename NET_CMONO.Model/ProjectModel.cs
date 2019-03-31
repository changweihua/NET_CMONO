using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Project")]
    public class ProjectModel : IdentityKeyEntity<int>
    {
        public string ProjectName { get; set; }
        public string ProjectSummary { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectDetail { get; set; }
        public string ProjectLink { get; set; }

        // [ForeignKey("CreatedBy")]
        // public virtual EmployeeModel CreatedUser { get; set; }

        // [ForeignKey("UpdatedBy")]
        // public virtual EmployeeModel UpdatedUser { get; set; }
    }
}