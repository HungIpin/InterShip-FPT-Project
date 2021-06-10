export class Certification {
    Id: string;
    Name: string;
    Description: string;
    TakenTimes: number;
    Image: string;
    Difficulty: string;
    SkillID: string;
  SkillinCertifications: any[];
}
export class SkillinCertification {
    skillId: string;
    name: string
    certificationId: string;
}

