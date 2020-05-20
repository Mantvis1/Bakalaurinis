import { NewActivity } from '../../models/new-activity';

export interface IWorkModal {
  workFormData: NewActivity,
  formTitle: string,
  formConfirmationButtonName: string;
}
