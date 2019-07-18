import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegistrationRequest } from '../model/entities/apiRequests/RegistrationRequest';
import { ApiSettingsService } from './api-settings.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient, private apiSettings: ApiSettingsService){ } 
      
    getData(request: RegistrationRequest){
      
      
      return this.http.post(this.apiSettings.getUrlRegistration(), request); 
        
    }
}
