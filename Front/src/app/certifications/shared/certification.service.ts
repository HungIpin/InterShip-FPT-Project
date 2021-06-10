import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Certification } from './certification.model';


@Injectable({
  providedIn: 'root'
})
export class CertificationService {

  private urlAPI = 'https://localhost:44374/api/certifications/';
  private urlAPI2 = 'https://localhost:44374/api/skillincertifications/';
  constructor(private http: HttpClient) { }

  getCertifications = async () => {
    try {
      return await this.http.get(this.urlAPI + "").toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  getDbSkill = async () => {
    try {
      return await this.http.get(this.urlAPI2 + "").toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  postCertification = async (certifications) => {
    try 
    {
      const formData: FormData = new FormData();
      if (certifications.Image)
      {
        formData.append('image-upload', certifications.Image);
      }
      formData.append('Id',certifications.Id);
      formData.append('Name',certifications.Name);
      formData.append('Description',certifications.Description);
      formData.append('TakenTimes',certifications.TakenTimes);
      formData.append('Difficulty',certifications.Difficulty);
      let skills: any[] = [];
      certifications.SkillinCertifications.forEach(element => {
        let data = {skillId: element.skillId, certificationId: element.certificationId}
        skills.push(data);
      });
      formData.append("SkillinCertifications", JSON.stringify(skills));
      
      console.log(formData);
      return await this.http.post(this.urlAPI, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  postDbSkill = async (certifications) => {
    try {
      const formData: FormData = new FormData();
      formData.append('skillId',certifications.SkillId);
      formData.append('certificationID',certifications.Id);
      console.log(formData)
      return await this.http.post(this.urlAPI2, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  deleteCertification = async(id)=>{
    try {
      return await this.http.delete(this.urlAPI + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  deleteSkill = async(id)=>{
    try {
      return await this.http.delete(this.urlAPI2 + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  //
  updateCertification = async (id, certifications) => {
    try {
      const formData: FormData = new FormData();
      if (certifications.Image)
      {
        formData.append('image-upload', certifications.Image);
      }
      formData.append('Id',certifications.Id);
      formData.append('Name',certifications.Name);
      formData.append('Description',certifications.Description);
      formData.append('TakenTimes',certifications.TakenTimes);
      formData.append('Difficulty',certifications.Difficulty);

      let skills: any[] = [];
      certifications.SkillinCertifications.forEach(element => {
        let data = {skillId: element.skillId, certificationId: element.certificationId}
        skills.push(data);
      });
      formData.append("SkillinCertifications", JSON.stringify(skills));
      
      return await this.http.put(this.urlAPI + id, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  updateDbSkill = async (certifications, id) => {
    try {
      const formData: FormData = new FormData();
      formData.append('certificationId',certifications.Id);
      formData.append('skillID',certifications.SkillID);
      console.log(formData)
      this.deleteSkill(certifications.id);
      this.postDbSkill(formData);
      return await this.http.put(this.urlAPI2 + id, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

}


