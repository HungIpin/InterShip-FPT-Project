import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { ScoresService } from '../score/shared/score.service';
import { Score } from '../score/shared/score.model';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';


@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoresComponent implements OnInit, OnDestroy {
  scores: Array<Score>
  settings: Object
  public Id: string=''
  public ExamId: string = ''
  public AccountId: string = ''
  public Points: string = ''
  public dataset: Score[]
  
  constructor(private service: ScoresService, private chRef : ChangeDetectorRef) { }

  ngOnDestroy(): void 
  {
    this.dtTrigger.unsubscribe();
  }

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  @ViewChild(DataTableDirective) dtElement: DataTableDirective;
  
  async ngOnInit(): Promise<void> {

    this.dtOptions = {
      pagingType: 'full_numbers',
      lengthMenu : [5, 10, 25, 30],
      processing: true
    };

    await this.reload();

    this.chRef.detectChanges();
    this.dtTrigger.next();
    
  }

  private reload = async () => {
    this.dataset = await this.getexams();
    console.log(this.dataset);
  }

  public getexams = async () => {
    const list = await this.service.getScores() as Score[];
    return list;

  }

  public deleteScore = async (examID) => {
    try 
    {
      const score = await this.service.deleteScores(examID);
      alert('delete successfully');
      await this.reload();
      this.rerender();
    }
    catch (e) {
      console.log(e);
    }

  }

  rerender() 
  {
    this.dtElement.dtInstance.then((dtInstance : DataTables.Api) => 
    {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }

}
