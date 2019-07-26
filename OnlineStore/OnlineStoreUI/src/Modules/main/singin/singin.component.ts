import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import {Subscription} from 'rxjs';
import { AuthorizationRequest } from 'src/model/entities/apiRequests/AuthorizationRequest';
import { AuthorizationService } from 'src/services/authorization.service';
import { AuthorizationResult } from 'src/model/entities/apiResults/AuthorizationResult';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ServerErrorsService } from 'src/services/server-errors.service';

@Component({
  selector: 'app-singin',
  templateUrl: 'singin.component.html',
  styleUrls: ['singin.component.css']
})
export class SinginComponent implements OnInit {

  authorizationMessage: string; 
  
  querySubscription: Subscription; 

  constructor(private router: Router,activateRoute: ActivatedRoute, 
    private authorizationService: AuthorizationService, 
    private localStorageService: LocalStorageService,
    private serverErrorsService: ServerErrorsService) { 
    
    this.querySubscription = activateRoute.queryParams.subscribe(
      (queryParam: any) => {
          this.authorizationMessage = queryParam['authorizationMessage'];
      }
    );

  }

  ngOnInit() {
  }

  authorization(login:string, password:string){
  


    let request: AuthorizationRequest = new AuthorizationRequest
      {
        request.login = login;
        request.password = password;
      };

     
      this.authorizationService.getData(request).subscribe((data: AuthorizationResult) => 
        this.processingServerResult(data),
        error => { 
          this.authorizationMessage = this.serverErrorsService.processError(error);
        });
  }

 

  processingServerResult(data:AuthorizationResult) {

    this.localStorageService.saveAuthorizationUser(data.userLogin, data.userRole.role.toString(), data.jwt, data.dateTimeAuthorizationFinish);

      this.router.navigate(['']
      , {
        queryParams:{
          'user': data.userLogin}
      });
      


   
  }






}
