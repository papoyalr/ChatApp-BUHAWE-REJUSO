using ChatApp.Models;
using System.Threading.Tasks;

namespace ChatApp.DependencyServices
{
    public interface iFirebaseAuth
    {
        Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password);
        Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password);
        FirebaseAuthResponseModel SignOut();
        FirebaseAuthResponseModel IsLoggedIn();
        Task<FirebaseAuthResponseModel> ResetPassword(string email);
    }
}