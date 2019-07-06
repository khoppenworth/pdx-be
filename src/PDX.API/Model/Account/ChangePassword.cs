namespace PDX.API.Model
{
    public class ChangePassword
    {
       public int UserID{get;set;}
       public string OldPassword{get;set;}
       public string NewPassword{get;set;}  
       public bool HasOldPassword{get;set;} = true;
    }
}