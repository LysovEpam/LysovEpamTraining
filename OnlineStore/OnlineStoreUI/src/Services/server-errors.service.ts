import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })

  export class ServerErrorsService {

    constructor(){ } 
        
    processError(error:any):string{

        if(error.status == 400)
            return  error.error;
        if(error.status == 401)
            return  'Authorization required to complete the action';
        if(error.status == 403)
            return 'You are not authorized to perform an action';
        if(error.status == 404)
            return 'The requested resource could not be found';
        if(error.status == 409)
            return error.error;
        if(error.status == 500)
            return error.error;
        if(error.status == 0)
            return 'Could not connect to server. Try again';

        return 'Server request error';
      }
  }