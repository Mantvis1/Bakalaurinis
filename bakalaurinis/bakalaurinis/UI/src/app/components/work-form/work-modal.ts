import { NewWork } from '../../models/new-work';

export interface IWorkModal {
  workFormData: NewWork,
  formTitle: string,
  formConfirmationButtonName: string;
}
