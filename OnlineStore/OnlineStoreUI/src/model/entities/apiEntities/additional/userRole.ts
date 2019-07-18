export class UserRole{
	role: UserRoleEnum;

	constructor(role:UserRoleEnum){
		this.role = role;
	}
}

export enum UserRoleEnum {
	User = 10,
	Editor = 11,
	Admin = 12
}