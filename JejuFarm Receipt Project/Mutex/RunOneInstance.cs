using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace JejuFarm_Receipt_Project.Mutex
{
    public class RunOneInstance
    {
        private System.Threading.Mutex mutex;

        public bool CreateOnlyOneMutex(string mutexName)
        {
            // Application GUID 를 이용해서 Mutext 고유한 이름을 만든다.
            var assembly = typeof(GuidAttribute).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            var id = attribute.Value;

            string appGuid = id.ToString();
            // Global prefix를 붙여서 global mutex를 만든다
            //string mutexId = string.Format("Global\\{{{0}}}", appGuid);
            string mutexId = "Global\\" + appGuid;


            // 보안 속성을 모든 사람이 사용할 수 있도록 셋팅한다.
            // 모든 사람이 접근 가능하므로 보안에 문제가 있을수 있다.
            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);


            bool canCreateNewMutex;
            //보안 속성을 추가해서 mutex를 생성한다.
            //public Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity       
            //mutexSecurity);

            mutex = new System.Threading.Mutex(false, mutexId, out canCreateNewMutex, securitySettings);

            return canCreateNewMutex;
        }
    }
}
