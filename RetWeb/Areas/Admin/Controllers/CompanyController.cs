using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;
using RetWeb.Models.ViewModels;
using RetWeb.Utility;

namespace RetWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]   //add authorization for only admin access to routes/pages
    public class CompanyController : Controller
    {
        /// <summary>
        /// use the ICompanyRepository rather than use the ApplicationDbContext here directly
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Companys
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().Where(c => c.IsDeleted == false).ToList();  // retrieve the list
            return View(objCompanyList);
        }

        /// <summary>
        /// Get Displays for Create or Edit Company Page
        /// <param name="id"></param>
        /// </summary>
        /// <returns> the view page for create or edit </returns>
        public IActionResult Upsert(int? id)
        {
            
            if (id == null || id == 0)
            {
                return View(new Company());
            }


            Company? Company = _unitOfWork.Company.Get(u => u.Id == id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }



        /// <summary>
        /// Post and Update method for the create Company form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upsert(Company obj)   // we have to modify the model to CompanyVM, the IFormFile gets the file input
        {

            // Check if the ModelState is valid before proceeding
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            string message;
            if (obj.Id == 0 || obj.Id == null)
            {
                //CREATE

                // Company creation
                _unitOfWork.Company.Add(obj);
                message = obj.Name + " created successfully.";

            }
            else   //UPDATE
            {

                // Check if the Company with the provided ID exists
                var existingCompany = _unitOfWork.Company.Get(u => u.Id == obj.Id);
                if (existingCompany == null)
                {
                    return View(obj); // Return back to the view the obj 
                }

                // Update the Company with the provided data
                existingCompany.Name = obj.Name;
                existingCompany.StreetAddress = obj.StreetAddress;
                existingCompany.City = obj.City;
                existingCompany.StateOrProvince = obj.StateOrProvince;
                existingCompany.PostalCode = obj.PostalCode;
                existingCompany.PhoneNumber = obj.PhoneNumber;

                _unitOfWork.Company.Update(existingCompany);
                message = obj.Name + " updated successfully.";

            }

            _unitOfWork.Save();
            TempData["success"] = message;    // this will send back a message notification to the index Company page 
            return RedirectToAction("Index", "Company");
        }

        // we create a region that handles our api calls 
        #region API Calls   
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().Where(c => c.IsDeleted == false).ToList();  // retrieve the list
            return Json(new { data = objCompanyList });
        }

        /// <summary>
        /// Delete a Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success="false", message="Error while deleting!"});
            }
            obj.IsDeleted = true;

            _unitOfWork.Company.Update(obj);
            _unitOfWork.Save();
            string msg = "Deleted " + obj.Name + " Company Successfully.";
            return Json(new { success = true, message = msg });
        }
        #endregion

    }
}
