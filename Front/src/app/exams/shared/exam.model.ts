import { DatePipe } from "@angular/common";

export class Exam 
{
    id: string;
    name: string;
    description: string;
    numOfQuestions: number;
    passScore: number;
    duration: number;
    rating: number;
    createdDate: Date;
    certificationId: string;
    certification: Certification
    feedbackTypeId: number;
    feedbackLevelId: number;
    accountId: number;
    scoreRecordingId: number;
}  
export class Certification 
{
    id: string;
    name: string;
    description: string;
    passScore: number;
    takenTimes: number;
    image: string;
    difficulty: string;    
}  