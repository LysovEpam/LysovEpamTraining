import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationService} from '../../../services/registration.service';
import { RegistrationRequest } from 'src/model/entities/apiRequests/RegistrationRequest';

@Component({
  selector: 'input-overview-example',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.css', '../../../app/app.component.css'],
  providers: [RegistrationService]
})


export class SingupComponent{ 
 

  registrationMessage: string;
  
  firstNameError: string;
  lastNameError: string;
  emailError: string;
  phoneError: string;
  loginError: string;
  passwordError: string;
  confirmPasswordError: string;
   
  constructor(private router: Router, private registrationService: RegistrationService){}

 
  registration(firstName: string, lastName: string, email: string, phone: string, login: string, password: string, confirmPassword: string){

    this.registrationMessage = "";

   
    if(this.validData(password, confirmPassword))
    {
      let request: RegistrationRequest = new RegistrationRequest
      {
        request.FirstName = firstName;
        request.LastName = lastName;
        request.Email = email;
        request.Phone = phone;
        request.Login = login;
        request.Password = password;
      };
   
      this.registrationService.getData(request).subscribe((data:any) => 
        {
          this.router.navigate(['/singin'], {
            queryParams:{ 'authorizationMessage': 'Registration was successful, need to log in'}});
        }, 
        error => { this.processingServerError(error);}
      );
    }
  }

  validData(password: string, confirmPassword: string):boolean{

    let validResult : boolean = true;

    if(password != confirmPassword){
      this.confirmPasswordError = "Need to confirm your password ";
      validResult = false;
    }else{
      this.confirmPasswordError = "";
    }

      
  return validResult;
    
  }



  processingServerError(error:any){ 
   
    this.registrationMessage = 'Server request error';

    if(error.status == 400)
      this.registrationMessage = error.error;
      if(error.status == 404)
      this.registrationMessage = 'The requested resource could not be found';
    if(error.status == 409){
      this.firstNameError = error.error.firstNameError;
      this.lastNameError= error.error.lastNameError;
      this.emailError= error.error.emailError;
      this.phoneError= error.error.phoneError;
      this.loginError= error.error.loginError;
      this.passwordError= error.error.passwordError;

      this.registrationMessage = 'Invalid data request!';
    }
     if(error.status == 500)
       this.registrationMessage = error.error;
    if(error.status == 0)
      this.registrationMessage = 'Could not connect to server. Try again';

  }

 
}
