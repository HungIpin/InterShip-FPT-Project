import { SkillinCertification } from "./certification.model";

export interface DialogData {
    Id: string;
    Name: string;
    Description: string;
    TakenTimes: number;
    Image: File;
    Difficulty: string;
    SkillinCertifications: SkillinCertification[];
}
