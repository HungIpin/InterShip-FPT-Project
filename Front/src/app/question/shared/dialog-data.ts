export class DialogData{
    id: number;
    questionText: string;
    questionTypeId: number;
    questionPoolId: number;
    selectionSettingId: number;
    questionSetting: QuestionSetting;
    questionChoices: QuestionChoice[];
    questionAttachments: QuestionAttachment[] 
}
export class QuestionSetting
{
    questionId: number;
    createdDate : Date;
    correctFb: string;
    inCorrectFb : string;
    displayPoint : boolean;
    pointValue : number;
    deductedPoints: number;
}
export class QuestionChoice
{
    choice: string;
    isCorrect : boolean;
    questionId: number;
}
export class QuestionAttachment
{
    name: string;
    attachment: any;
    questionId: number;
}