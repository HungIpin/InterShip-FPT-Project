import { Question } from "../question/shared/question.model";

export class ExamPart
{
    id: number;
    examId: number;
    name: number;
    questionsInParts: QuestionsInPart[];
}
export class QuestionsInPart
{
    id: number;
    questionId: number;
    examPartId: number;
    sequenceNo: number;
    question: Question
}