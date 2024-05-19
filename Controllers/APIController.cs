using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using FenixLauncher.Models;
using System;
using System.Management;
using FenixLauncher.Data;
using FenixLauncher.Functions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FenixLauncher.Controllers
{
    [Route("api")]
    public class APIController : Controller
    {
        AccessData accessData = new AccessData();
        UserData userData = new UserData();
        Security security = new Security();
        SysProcesses sysProcesses = new SysProcesses();
        ProcessData processData = new ProcessData();
        TaskData taskData = new TaskData();
        LogData logData = new LogData();
        Validations validations = new Validations();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel data)
        {
            string user = data.User;
            string pwd = data.Pwd;

            if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pwd))
            {
                if (accessData.Login(data))
                {
                    UserModel userModel = userData.GetByName(data.User);
                    string token = security.GenerateTokenJWT(userModel.Id, userModel.User);
                    HttpContext.Session.SetString(new Configuration().session_name, token);
                    return Json(new { status = 1, message = "Successful login", token = token });
                }
                else
                {
                    return Json(new { status = 0, message = "Bad login" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Missing data" });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                HttpContext.Session.Remove(new Configuration().session_name);
                return Json(new { status = 1, message = "Successful login" });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("data")]
        public IActionResult ListData()
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "Data listed", processes = processData.List(""), tasks = taskData.List("") });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("processes")]
        public IActionResult ListProcesses()
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "Processes listed", processes = processData.List("") });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("processes/{id}")]
        public IActionResult LoadProcess(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "Process loaded", process = processData.Get(id) });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("processes/{id}/tasks")]
        public IActionResult LoadTasksByProcess(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "Tasks loaded", tasks = taskData.ListByProcess(id) });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpPost("processes")]
        public IActionResult CreateProcess([FromBody] ProcessInfoModel data)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                if (validations.validateCreateProcess(data))
                {
                    processData.Add(data);
                    return Json(new { status = 1, message = "Process created" });
                }
                else
                {
                    return Json(new { status = 0, message = "Missing data" });
                }
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpPut("processes/{id}")]
        public IActionResult EditProcess(int id, [FromBody] ProcessInfoModel data)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                data.Id = id;
                processData.Update(data);
                if (validations.validateEditProcess(data))
                {
                    return Json(new { status = 1, message = "Process updated" });
                }
                else
                {
                    return Json(new { status = 0, message = "Missing data" });
                }
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpDelete("processes/{id}")]
        public IActionResult DeleteProcess(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                processData.Delete(new ProcessInfoModel { Id = id });
                return Json(new { status = 1, message = "Process deleted" });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("tasks")]
        public IActionResult ListTasks()
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "OK", tasks = taskData.List("") });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("tasks/{id}")]
        public IActionResult LoadTask(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { status = 1, message = "Task loaded", task = taskData.Get(id) });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpPost("tasks")]
        public IActionResult CreateTask([FromBody] TaskInfoModel data)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                if (validations.validateCreateTask(data))
                {
                    taskData.Add(data);
                    return Json(new { status = 1, message = "Task created" });
                }
                else
                {
                    return Json(new { status = 0, message = "Missing data" });
                }
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpPut("tasks/{id}")]
        public IActionResult EditTask(int id, [FromBody] TaskInfoModel data)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                data.Id = id;
                taskData.Update(data);
                if (validations.validateEditTask(data))
                {
                    return Json(new { status = 1, message = "Task updated" });
                }
                else
                {
                    return Json(new { status = 0, message = "Missing data" });
                }
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpDelete("tasks/{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                taskData.Delete(new TaskInfoModel { Id = id });
                return Json(new { status = 1, message = "Task deleted" });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpPost("account")]
        public IActionResult Account([FromBody] AccountModel data)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                if (validations.validateUpdateAccount(data))
                {
                    string current_user = data.CurrentUser;
                    string new_username = data.NewUsername;
                    string new_password = data.NewPassword;
                    string current_password = data.CurrentPassword;

                    LoginModel loginModel = new LoginModel();
                    loginModel.User = current_user;
                    loginModel.Pwd = current_password;

                    if (accessData.Login(loginModel))
                    {
                        int id = userData.GetByName(current_user).Id;

                        if (new_username != "")
                        {
                            ChangeUsernameModel changeUsernameModel = new ChangeUsernameModel();
                            changeUsernameModel.Id = id;
                            changeUsernameModel.NewUsername = new_username;
                            userData.ChangeUsername(changeUsernameModel);
                        }

                        if (new_password != "")
                        {
                            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
                            changePasswordModel.Id = id;
                            changePasswordModel.NewPassword = BCrypt.Net.BCrypt.HashPassword(new_password);
                            userData.ChangePasssword(changePasswordModel);
                        }

                        HttpContext.Session.Remove(new Configuration().session_name);

                        return Json(new { status = 1, message = "Account updated" });
                    }
                    else
                    {
                        return Json(new { status = 2, message = "Bad login" });
                    }
                }
                else
                {
                    return Json(new { status = 0, message = "Missing data" });
                }
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("processes/execute/{id}")]
        public IActionResult ExecuteProcess(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                // Load data

                ProcessInfoModel processInfo = processData.Get(id);

                List<TaskInfoModel> tasks = taskData.ListByProcess(processInfo.Id);

                // Clean pids

                string output = "";

                foreach (TaskInfoModel task in tasks)
                {
                    List<LogInfoModel> logs = logData.ListByTask(task.Id);
                    foreach (LogInfoModel log in logs)
                    {
                        sysProcesses.KillProcessesByPID(log.Pid);
                        logData.Delete(new LogInfoModel { Id = log.Id });
                        output += "[+] Process Details : " + log.Details + " PID : " + log.Pid + " closed" + Environment.NewLine;
                    }
                }

                // Execute processes

                List<int> arrayPids = new List<int>();

                foreach (TaskInfoModel taskInfo in tasks)
                {
                    Process p = new Process();
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.WorkingDirectory = taskInfo.Directory;
                    info.FileName = "cmd.exe";

                    if (taskInfo.UAC == 1)
                    {
                        p.StartInfo.Verb = "runas";
                    }

                    info.Arguments = "/C " + taskInfo.Command;

                    info.UseShellExecute = true;
                    info.CreateNoWindow = false;

                    int window_style = taskInfo.WindowStyle;

                    if (window_style == 1)
                    {
                        info.CreateNoWindow = true;
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                    }
                    else if (window_style == 2)
                    {
                        info.WindowStyle = ProcessWindowStyle.Normal;
                    }
                    else if (window_style == 3)
                    {
                        info.WindowStyle = ProcessWindowStyle.Minimized;
                    }
                    else if (window_style == 4)
                    {
                        info.WindowStyle = ProcessWindowStyle.Maximized;
                    }
                    else
                    {
                        info.WindowStyle = ProcessWindowStyle.Normal;
                    }

                    p.StartInfo = info;
                    p.Start();

                    logData.Add(
                        new LogInfoModel
                        {
                            Details = processInfo.Name + " - " + taskInfo.Name,
                            Pid = p.Id,
                            Id_task = taskInfo.Id
                        }
                    );

                    arrayPids.Add(p.Id);
                }

                if (processInfo.URL != "")
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(processInfo.Timeout_URL * 1000);
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = processInfo.URL,
                            UseShellExecute = true
                        });
                    });
                }

                return Json(new { status = 1, message = "Tasks executed", pids = arrayPids, output = output });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("logs")]
        public IActionResult ListLogs()
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                List<ProcessInfoModel> logs_processes = new List<ProcessInfoModel>();
                List<int> arrayIdsProcesses = new List<int>();
                List<LogInfoModel> logs = logData.List("");
                foreach(LogInfoModel log in logs)
                {
                    TaskInfoModel task = taskData.Get(log.Id_task);
                    ProcessInfoModel process = processData.Get(task.IdProcess);
                    if (!arrayIdsProcesses.Contains(task.IdProcess))
                    {
                        logs_processes.Add(process);
                        arrayIdsProcesses.Add(task.IdProcess);
                    }
                }
                return Json(new { status = 1, message = "Logs listed", logs_tasks = logs, logs_processes = logs_processes });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("processes/close/{id}")]
        public IActionResult killProcess(int id)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                List<TaskInfoModel> tasks = taskData.ListByProcess(id);

                string output = "";

                foreach (TaskInfoModel task in tasks)
                {
                    List<LogInfoModel> logs = logData.ListByTask(task.Id);
                    foreach (LogInfoModel log in logs)
                    {
                        sysProcesses.KillProcessesByPID(log.Pid);
                        logData.Delete(new LogInfoModel { Id = log.Id });
                        output += "[+] Process Details : " + log.Details + " PID : " + log.Pid + " closed" + Environment.NewLine;
                    }
                }

                return Json(new { status = 1, message = "Process closed", output = output });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

        [HttpGet("tasks/close/{pid}")]
        public IActionResult killTaskByPID(int pid)
        {
            if (security.ValidateTokenLogin(Request.Headers["Authorization"].ToString()))
            {
                sysProcesses.KillProcessesByPID(pid);
                logData.DeleteByPID(new LogInfoModel { Pid = pid });
                return Json(new { status = 1, message = "PID " + pid + " closed" });
            }
            else
            {
                return Json(new { status = 0, message = "Access denied" });
            }
        }

    }
}
