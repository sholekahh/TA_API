using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api_mod9.Models;
using api_mod9.Models.Dto;
using api_mod9.Data;
using percobaan_mod9_kel04.NewFolder;
using percobaan_mod9_kel04.Models.NewFolder1;

namespace percobaan_mod9_kel04.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(200, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) return BadRequest();
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null) return NotFound();
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError(" CustomError ", " Villa already exists  ");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            string response = " Sukses menambahkan data Vila" + "\nId: " + villaDTO.Id.ToString() + "\nNama: " + villaDTO.Name;

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, response);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpGet("users", Name = "GetUsers")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(UserStore.userList);
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> Login([FromBody] UserDTO user)
        {
            var existingUser = UserStore.userList.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser != null)
            {
                return Ok("You are now logged in");
            }
            else
            {
                return BadRequest("Invalid username or password");
            }
        }

        [HttpPost("create")]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO userDTO)
        {
            if (UserStore.userList.FirstOrDefault(u => u.Username.ToLower() == userDTO.Username.ToLower()) != null)
            {
                ModelState.AddModelError(" CustomError ", " User already exists  ");
                return BadRequest(ModelState);
            }
            if (userDTO == null)
            {
                return BadRequest(userDTO);
            }
            if (userDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            userDTO.Id = UserStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            UserStore.userList.Add(userDTO);
            string response = " Sukses menambahkan data User" + "\nId: " + userDTO.Id.ToString() + "\nUsername: " + userDTO.Username;

            return Ok(response);
        }
    }
}
