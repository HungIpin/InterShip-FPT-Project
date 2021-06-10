import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { async } from 'rxjs/internal/scheduler/async';
@Injectable({
  providedIn: 'root'
})
export class SkillsService {
  private urlAPI = 'https://localhost:44374/api/skills/';

  constructor(private http: HttpClient) {

  }
  

  getSkills = async () => {
    try {
      return await this.http.get(this.urlAPI + "").toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  postSkills = async (skill) => {
    try {
      
      
    const formData: FormData = new FormData();
  
    if (skill.Image) {

      formData.append('image-upload', skill.Image);
    }
    formData.append('Id', skill.Id);
    formData.append('Name',skill.Name);
    formData.append('Description',skill.Description);//sao nay undefined co a

    console.log(formData);

      return await this.http.post(this.urlAPI, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  deleteSkills = async(id)=>{
    try {
      return await this.http.delete(this.urlAPI + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  //
  updateSkills = async (id, skill) => {
    try {

      const formData: FormData = new FormData();
  
    if (skill.Image) {

      formData.append('image-upload', skill.Image);
    }

    formData.append('Id', skill.Id);
    formData.append('Name',skill.Name);
    formData.append('Description',skill.Description);//sao nay undefined co a


      return await this.http.put(this.urlAPI + id, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
}
