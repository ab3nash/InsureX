import { Dayjs } from "dayjs";

export interface ApplicantDetails {
    name: string;
    occupation: string;
    dateOfBirth: Dayjs;
    age: number;
    sumInsured: number;
}