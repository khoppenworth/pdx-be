using System;

namespace PDX.BLL.Services.Notification {
        public static class AlertTemplates {

            public static string MarketAuthorizationStatusChange {
                get {
                    return "Market Authorization has been {0}</br> Order no:{1}</br>Status:{0}</br>Date:{2}</br> Check Your Order";
                }
            }
            public static string FurtherInformationRequested {
                get {
                    return "Please Provide further information for application no:{1}</br>Date:{2}</br>";
                }
            }
            public static string AssessmentNotFinished {
                get {
                    return "{0} has not finished assessing the {1} application no";
                }

            }
            public static string ImportPermitStatusChange {
                get {
                    return "Import permit has been {0}</br> Order no:{1}</br>Status:{0}</br>Date:{2}</br> Check Your Order";
                }
            }
        }
}