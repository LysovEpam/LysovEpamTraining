export class UserRole{
	role: UserRoleEnum;

	constructor(role:UserRoleEnum){
		this.role = role;
	}

	static getRoleListName(){
        let allStatus:string[]=[
            this.getRolePrint(UserRoleEnum.User),
            this.getRolePrint(UserRoleEnum.Editor),
            this.getRolePrint(UserRoleEnum.Admin)
        ];

        return allStatus;
	}
	
	static getRolePrint(status:UserRoleEnum){

        if(status == UserRoleEnum.User)
            return 'User';
        if(status == UserRoleEnum.Editor)
            return 'Editor';
        if(status == UserRoleEnum.Admin)
            return 'Admin';
               
        return '';
    }
}

export enum UserRoleEnum {
	User = 10,
	Editor = 11,
	Admin = 12
}