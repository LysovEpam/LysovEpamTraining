
export class UserStatus {
	status:UserStatusEnum;

	constructor(status:UserStatusEnum){
		this.status = status;
    }
    
    static getStatusListName(){
        let allStatus:string[]=[
            this.getStatusPrint(UserStatusEnum.Active),
            this.getStatusPrint(UserStatusEnum.Block),
            this.getStatusPrint(UserStatusEnum.Delete)
        ];

        return allStatus;
    }

	static getStatusPrint(status:UserStatusEnum){

        if(status == UserStatusEnum.Active)
            return 'Active';
        if(status == UserStatusEnum.Block)
            return 'Block';
        if(status == UserStatusEnum.Delete)
            return 'Delete';
               
        return '';
    }
}

export enum UserStatusEnum {
	Active = 10,
	Block = 11,
	Delete =12 
}

