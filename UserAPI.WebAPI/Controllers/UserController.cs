using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Application.Users.Commands.CreateUser;
using UserAPI.Application.Users.Commands.FullDeleteUser;
using UserAPI.Application.Users.Commands.RestoreUser;
using UserAPI.Application.Users.Commands.SoftDeleteUser;
using UserAPI.Application.Users.Commands.UpdateUser;
using UserAPI.Application.Users.Commands.UpdateUserLogin;
using UserAPI.Application.Users.Commands.UpdateUserPassword;
using UserAPI.Application.Users.Queries.GetActiveUserList;
using UserAPI.Application.Users.Queries.GetUserByAge;
using UserAPI.Application.Users.Queries.GetUserByLogin;
using UserAPI.Application.Users.Queries.GetUserByLoginAndPassword;
using UserAPI.Domain;
using UserAPI.WebAPI.Models;

namespace UserAPI.WebAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class UserController : BaseController
	{
		private readonly IMapper _mapper;

		public UserController(IMapper mapper)
		{
			_mapper = mapper;
		}

		/// <summary>
		/// Получить всех активных пользователей (доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример простого запроса:
		/// GET /user/admin/adminLogin/adminPassword
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латински буквы и цифры) - string</param>
		/// <returns>Возвращает UserListVm</returns>
		/// <response code="200">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		[HttpGet("admin/{adminLogin}/{adminPassword}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<UserListVm>> GetActiveUsers(string adminLogin, string adminPassword)
		{
			var query = new GetActiveUserListQuery { AdminLogin = adminLogin, AdminPassword = adminPassword };
			var vm = await Mediator.Send(query);

			return Ok(vm);
		}

		/// <summary>
		/// Получить всех пользователей старше чем "age" (доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример простого запроса:
		/// GET /user/admin/adminLogin/adminPassword/17
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латинские буквы и цифры) - string</param>
		/// <param name="age">Возраст - int</param>
		/// <returns>Возвращает UserListVm</returns>
		/// <response code="200">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		[HttpGet("admin/getUserByAge/{adminLogin}/{adminPassword}/{age}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<UserListVm>> GetUsersByAge(string adminLogin, string adminPassword, int age)
		{
			var query = new GetUserListByAgeQuery { AdminLogin = adminLogin, AdminPassword = adminPassword, Age = age };
			var vm = await Mediator.Send(query);

			return Ok(vm);
		}

		/// <summary>
		/// Получить пользователя по логину(доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример простого запроса:
		/// GET /user/admin/adminLogin/adminPassword/userLogin
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латинские буквы и цифры) - string</param>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <returns>Возвращает UserVm</returns>
		/// <response code="200">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		[HttpGet("admin/{adminLogin}/{adminPassword}/{login}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserVm>> GetUserByLogin(string adminLogin, string adminPassword, string login)
		{
			var query = new GetUserByLoginQuery { AdminLogin = adminLogin, AdminPassword = adminPassword, Login = login };
			var vm = await Mediator.Send(query);

			return Ok(vm);
		}

		/// <summary>
		/// Получить пользователя по логину и паролю
		/// </summary>
		/// <remarks>
		/// Пример простого запроса:
		/// GET /user/admin/userLogin/userPassword
		/// </remarks>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="password">Пароль пользователя(только латинские буквы и цифры) - string</param>
		/// <returns>Возвращает User</returns>
		/// <response code="200">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="403">Forbidden(пользователь не активен)</response>
		[HttpGet("{login}/{password}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<User>> GetUser(string login, string password)
		{
			var query = new GetUserByLoginAndPasswordQuery { Login = login, Password = password };
			var vm = await Mediator.Send(query);

			return Ok(vm);
		}

		/// <summary>
		/// Создать пользователя(доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// POST /user/admin/adminLogin/adminPassword
		/// {
		///		login: "user",
		///		password: "userPassword123",
		///		name: "userName",
		///		gender: 1,
		///		birthDay: "2004-05-13T09:13:19.166Z",
		///		admin: false
		/// }
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латинские буквы и цифры) - string</param>
		/// <param name="createUserDto">Объект CreateUserDto</param>
		/// <returns>Возвращает id(guid) созданного пользователя</returns>
		/// <response code="201">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		/// <response code="409">Conflict(такой логин уже существует)</response>
		[HttpPost("admin/{adminLogin}/{adminPassword}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<ActionResult<Guid>> CreateUser(string adminLogin, string adminPassword, CreateUserDto createUserDto)
		{
			var command = _mapper.Map<CreateUserCommand>(createUserDto);
			command.AdminLogin = adminLogin;
			command.AdminPassword = adminPassword;
			var userId = await Mediator.Send(command);

			return Ok(userId);
		}

		/// <summary>
		/// Обновить пользователя(доступно только админам или самому пользователю если он активен)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// PUT /user/userLogin/userPassword
		/// {
		///		userId: "C29E7F3B-11A4-47AA-87E5-3406F8400DFC",
		///		name: "userName",
		///		gender: 1,
		///		birthDay: "2004-05-13T09:13:19.166Z",
		/// }
		/// </remarks>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="password">Пароль пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="updateUserDto">Объект UpdateUserDto</param>
		/// <returns>Возвращает NoContent</returns>
		/// <response code="204">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		/// <response code="403">Forbidden(недостаточно прав или пользователь не активен)</response>
		[HttpPut("{login}/{password}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateUser(string login, string password, UpdateUserDto updateUserDto)
		{
			var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
			command.SenderLogin = login;
			command.SenderPassword = password;
			await Mediator.Send(command);

			return NoContent();
		}

		/// <summary>
		/// Обновить пароль пользователя(доступно только админам или самому пользователю если он активен)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// PUT /user/updatePassword/userLogin/userPassword
		/// {
		///		userId: "C29E7F3B-11A4-47AA-87E5-3406F8400DFC",
		///		password: "userPassword",
		/// }
		/// </remarks>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="password">Пароль пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="updateUserPasswordDto">Объект UpdateUserPasswordDto</param>
		/// <returns>Возвращает NoContent</returns>
		/// <response code="204">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		/// <response code="403">Forbidden(недостаточно прав или пользователь не активен)</response>
		[HttpPut("updatePassword/{login}/{password}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdatePassword(string login, string password, UpdateUserPasswordDto updateUserPasswordDto)
		{
			var command = _mapper.Map<UpdateUserPasswordCommand>(updateUserPasswordDto);
			command.SenderLogin = login;
			command.SenderPassword = password;
			await Mediator.Send(command);

			return NoContent();
		}

		/// <summary>
		/// Обновить логин пользователя(доступно только админам или самому пользователю если он активен)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// PUT /user/updateLogin/userLogin/userPassword
		/// {
		///		userId: "C29E7F3B-11A4-47AA-87E5-3406F8400DFC",
		///		login: "userLogin",
		/// }
		/// </remarks>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="password">Пароль пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="updateUserLoginDto">Объект UpdateUserLoginDto</param>
		/// <returns>Возвращает NoContent</returns>
		/// <response code="204">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		/// <response code="403">Forbidden(недостаточно прав или пользователь не активен)</response>
		[HttpPut("updateLogin/{login}/{password}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UpdateLogin(string login, string password, UpdateUserLoginDto updateUserLoginDto)
		{
			var command = _mapper.Map<UpdateUserLoginCommand>(updateUserLoginDto);
			command.SenderLogin = login;
			command.SenderPassword = password;
			await Mediator.Send(command);

			return NoContent();
		}

		/// <summary>
		/// Востановление пользователя (доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// PUT /user/admin/adminLogin/adminPassword/userLogin
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латинские буквы и цифры) - string</param>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <returns>Возвращает NoContent</returns>
		/// <response code="204">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		[HttpPut("admin/{adminLogin}/{adminPassword}/{login}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> RestoreUser(string adminLogin, string adminPassword, string login)
		{
			var command = new RestoreUserCommand { AdminLogin = adminLogin, AdminPassword = adminPassword, Login = login };
			await Mediator.Send(command);

			return NoContent();
		}

		/// <summary>
		/// Полное или мягкое удаление пользователя по логину (доступно только админам)
		/// </summary>
		/// <remarks>
		/// Пример просто запроса:
		/// DELETE /user/admin/adminLogin/adminPassword/userLogin/true
		/// </remarks>
		/// <param name="adminLogin">Логин админа(только латинские буквы и цифры) - string</param>
		/// <param name="adminPassword">Пароль админа(только латинские буквы и цифры) - string</param>
		/// <param name="login">Логин пользователя(только латинские буквы и цифры) - string</param>
		/// <param name="isSoftDelete">Мягкое удаление(true), полное удаление(false) - bool</param>
		/// <returns>Возвращает NoContent</returns>
		/// <response code="204">Succes</response>
		/// <response code="400">Bad request(запрос не прошел валидацию)</response>
		/// <response code="401">Unauthorized(не правильный логин или пароль)</response>
		/// <response code="404">Not found(пользователь не найден)</response>
		/// <response code="403">Forbidden(недостаточно прав)</response>
		[HttpDelete("admin/{adminLogin}/{adminPassword}/{login}/{isSoftDelete}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> FullDelete(string adminLogin, string adminPassword, string login, bool isSoftDelete)
		{
			var fullDeleteCommand = new FullDeleteUserCommand { AdminLogin = adminLogin, AdminPassword = adminPassword, Login = login };
			var softDeleteCommand = new SoftDeleteUserCommand { AdminLogin = adminLogin, AdminPassword = adminPassword, Login = login };

			if (isSoftDelete) await Mediator.Send(softDeleteCommand);
			else await Mediator.Send(fullDeleteCommand);

			return NoContent();
		}
	}
}
