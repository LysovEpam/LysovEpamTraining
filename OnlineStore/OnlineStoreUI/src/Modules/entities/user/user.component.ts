import { Component, OnInit } from '@angular/core';
import { Subscription, identity } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/services/localstorage.service';
import {MatDialog } from '@angular/material/dialog';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';
import { UserRoleEnum, UserRole } from 'src/model/entities/apiEntities/additional/userRole';
import { UserStatus } from 'src/model/entities/apiEntities/additional/userStatus';
import { UserServerService } from 'src/services/user-server.service';

enum typeActionEnum{
  create,
  update,
  updateByMyself,
  undefined
}

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  querySubscription: Subscription;
  typeActionEnum:typeActionEnum;

  buttonSaveText:string;
  userMessage: string;
  changeStatusRoleIsEnable:boolean;
  changeLoginIsEnable:boolean;
  changePasswordIsEnable:boolean;
  confirmnewPassword:string;

  allStatus:string[];
  allRoles:string[]; 
  systemUser:SystemUserData;

  constructor(activateRoute: ActivatedRoute, private localStorageService: LocalStorageService, public dialog: MatDialog,
    private userServer: UserServerService,
    private serverErrorsService:ServerErrorsService) { 

    this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  }

  ngOnInit() {
    
    this.confirmnewPassword = "";
    this.allStatus = UserStatus.getStatusListName();
    this.allRoles = UserRole.getRoleListName();

    this.systemUser = new SystemUserData(0,'','','','',this.allStatus[0],this.allRoles[0],'','','');
  }

  
  


  processingQueryParam(queryParam: any){

    this.typeActionEnum = typeActionEnum.updateByMyself;
    this.buttonSaveText = '';

    var paramEntityAction = queryParam['action'];
    
    if(paramEntityAction!=null && paramEntityAction == 'create')
    {
      this.typeActionEnum = typeActionEnum.create;
      this.buttonSaveText = 'Create new entity';
      this.changeStatusRoleIsEnable = true;
      this.changeLoginIsEnable = true;
      this.changePasswordIsEnable = true;
    }
    else if(paramEntityAction!=null && paramEntityAction == 'update')
    {
      var paramEntityAction = queryParam['action'];
      let userRoleName: string = this.localStorageService.getUserRole();
      let role: UserRoleEnum = UserRoleEnum[userRoleName];
      let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

      if(userRoleEnum == UserRoleEnum.Admin){
        this.changeStatusRoleIsEnable = true;
      }
      else {
        this.userMessage = 'Access denied, only administrator can edit user information';
        this.changeStatusRoleIsEnable = false;
        this.buttonSaveText = '';
        this.typeActionEnum = typeActionEnum.undefined;
      }
      this.changePasswordIsEnable = false;
      this.changeLoginIsEnable = false;
      this.typeActionEnum = typeActionEnum.update;
      this.buttonSaveText = 'Update entity';

      var paramEntityIdEntity = queryParam['idEntity'];

      if(((paramEntityIdEntity != null) && !isNaN(Number(paramEntityIdEntity))))
      {
        var idEntity: number = Number(paramEntityIdEntity);
        this.loadById(idEntity);
      }
      else  {
        this.userMessage = 'Incorrect request, user can not be changed';
      }

    }
    else
    {

      this.typeActionEnum = typeActionEnum.updateByMyself;
      this.buttonSaveText = 'Update my information';
      this.changeStatusRoleIsEnable = false;
      this.changeLoginIsEnable = false;
      this.changePasswordIsEnable = true;
      this.loadByMyself();
      

    }
  }

  loadById(id:number){

    let jwt:string = this.localStorageService.getJwt();

    this.userServer.getById(id, jwt).subscribe((data: SystemUserData) =>{
      this.systemUser = data; },
      error => { 
        this.userMessage = this.serverErrorsService.processError(error);
      } );

  }

  loadByMyself(){
    
    let jwt:string = this.localStorageService.getJwt();

    this.userServer.getByToken(jwt).subscribe((data: SystemUserData) =>{
      this.systemUser = data; },
      error => { 
        this.userMessage = this.serverErrorsService.processError(error);
      } );

  }

  saveUser(){

    this.userMessage = "";

    
    if(this.systemUser.newPassword!= this.confirmnewPassword){
      this.userMessage = "You must confirm the new password!";
      this.systemUser.newPassword = "";
      this.systemUser.oldPassword = "";
      this.confirmnewPassword = "";
      return;
    }

    let jwt:string = this.localStorageService.getJwt();

    if(this.typeActionEnum == typeActionEnum.create){

          this.userServer.create(this.systemUser, jwt).subscribe((data:any) => {
            this.userMessage = 'User successfully created';
            this.systemUser = new SystemUserData(0,'','','','',this.allStatus[0],this.allRoles[0],'','','');
            this.confirmnewPassword = "";
          },
          error => {
            this.userMessage = this.serverErrorsService.processError(error); } );
    }

    else if(this.typeActionEnum == typeActionEnum.update ||
      this.typeActionEnum == typeActionEnum.updateByMyself){

      this.userServer.update(this.systemUser, jwt).subscribe((data:any) => {
        this.userMessage = 'User successfully updated';
        this.confirmnewPassword = "";
      },
      error => {
        this.userMessage = this.serverErrorsService.processError(error); } );
    }

    this.systemUser.newPassword = "";
    this.systemUser.oldPassword = "";
    this.confirmnewPassword = "";


  }


}
