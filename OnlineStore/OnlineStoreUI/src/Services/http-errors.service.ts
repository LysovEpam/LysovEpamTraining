// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { AuthorizationRequest } from 'src/model/entities/apiRequests/AuthorizationRequest';
// import { ApiSettingsService } from './api-settings.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class ProcessHttpErrorsService {

//   constructor(){ }

// processError(error:any): string{

//     if(error.status == 0)
//         return "Could not connect to server. Try again";
//     if(error.status == 401)
//         return "Authorization required"; 
//     if(error.status == 403)
//         return "you are not authorized for this action";
//     if(error.status == 404)
//         return "The requested resource could not be found";
//     if(error.status == 408)
//         return "Client transfer timeout from client expired";

//     return "Unknown error";
// }
      
    
// }