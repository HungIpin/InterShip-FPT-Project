export class Exam {
    id: string;
    name: string;
    description: string;
    image: string;
    base64: string;
    rating: number;
}

export class ExamPart {
    id: number;
    exam:Exam;
    name: string;
    sequenceNo: number;
    questionPoolId:number
    numOfQuestion: number;
    questionPoints: number;
    deductedPoints: number;
    isShuffle:boolean;
}

export class QuestionInPart{
    id:number;
    question:Question;
    questionId:number;
    examPart:ExamPart;
    examPartId:number;
    sequenceNo:number;
}

export class Question{
    id: number;
    questionText: string;
    questionTypeId: number;
    questionPoolId: number;
    selectionSettingId: number;
    createdDate : Date;
    correctFb: string;
    inCorrectFb : string;
    choice : string;
    displayPoint : boolean;
    pointValue : Float32Array;
}