//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanDetails
    {
        public long ID { get; set; }
        public string Training_contant { get; set; }
        public string Training_Task { get; set; }
        public string Training_Time { get; set; }
        public string Training_Source { get; set; }
        public string Training_strategy { get; set; }
        public Nullable<long> Plan_ID { get; set; }
    
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
