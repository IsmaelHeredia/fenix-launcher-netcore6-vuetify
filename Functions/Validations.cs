using FenixLauncher.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FenixLauncher.Functions
{
    public class Validations
    {
        public bool validateCreateTask(TaskInfoModel task)
        {
            bool response = true;

            if (string.IsNullOrEmpty(task.Name))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(task.Directory))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(task.Command))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.WindowStyle)))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.UAC)))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.IdProcess)))
            {
                response = false;
            }

            return response;
        }

        public bool validateEditTask(TaskInfoModel task)
        {
            bool response = true;

            if (string.IsNullOrEmpty(task.Name))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(task.Directory))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(task.Command))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.WindowStyle)))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.UAC)))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(task.IdProcess)))
            {
                response = false;
            }

            return response;
        }

        public bool validateCreateProcess(ProcessInfoModel process)
        {
            bool response = true;

            if (string.IsNullOrEmpty(process.Name))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(process.Timeout_URL)))
            {
                response = false;
            }

            return response;
        }

        public bool validateEditProcess(ProcessInfoModel process)
        {
            bool response = true;

            if (string.IsNullOrEmpty(process.Name))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(process.Timeout_URL)))
            {
                response = false;
            }

            return response;
        }

        public bool validateUpdateAccount(AccountModel updateAccount)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(updateAccount.NewUsername))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(updateAccount.NewPassword))
            {
                respuesta = false;
            }

            return respuesta;
        }
    }
}
