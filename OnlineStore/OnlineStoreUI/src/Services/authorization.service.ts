import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizationRequest } from 'src/model/entities/apiRequests/AuthorizationRequest';
import { ApiSettingsService } from './api-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    getData(request: AuthorizationRequest){
      
      
      return this.http.post(this.apiSettings.getUrlAuthorization(), request); 
        
    }
}
