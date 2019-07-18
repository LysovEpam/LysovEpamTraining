
export class UserStatus {
	status:UserStatusEnum;

	constructor(status:UserStatusEnum){
		this.status = status;
	}
}

export enum UserStatusEnum {
	Active,
	Block,
	Delete 
}

