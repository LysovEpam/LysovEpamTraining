import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { Router } from '@angular/router';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';
import { UserServerService } from 'src/Services/user-server.service';
import { ServerErrorsService } from 'src/Services/server-errors.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {

  accountMessage:string;

  systemUserData:SystemUserData;


  constructor(private router: Router,
    private localStorageService: LocalStorageService, 
    private userServerService:UserServerService, 
    private serverErrorService: ServerErrorsService) { }

  ngOnInit() {
    this.systemUserData = new SystemUserData('','','','','','','','');

    if(this.localStorageService.getAuthorizationWordDate() < new Date(Date.now()))
    {
      this.router.navigate(['/singin'], {
        queryParams:{
          'authorizationMessage': "Access required authorization"}
      });
    }

    let jwt:string = this.localStorageService.getJwt();

    this.userServerService.getByMyself(jwt).subscribe((data:SystemUserData) => {
      this.systemUserData = data;
    },
    error => { 
      this.accountMessage = this.serverErrorService.processError(error); } 
    );







  }

  saveNewInformation(firstName:string, lastName:string, email:string, phone:string, login:string, password:string, confirmPassword:string){
    this.accountMessage = '';
    if(password !=confirmPassword){
      this.accountMessage = 'Need to confirm your password';
      return;
    }

    let jwt:string = this.localStorageService.getJwt();

    this.accountMessage = 'need to save in server!!!!!!!!!!!';

  }










}
