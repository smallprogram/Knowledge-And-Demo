import { Component, OnInit } from '@angular/core';
import { Crisis } from '../class/crisis';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'src/app/service/dialog.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-crisis-detail',
  templateUrl: './crisis-detail.component.html',
  styleUrls: ['./crisis-detail.component.scss']
})
export class CrisisDetailComponent implements OnInit {

  crisis: Crisis;
  editName: string;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.route.data
    .subscribe((data: { crisis: Crisis }) => {
      this.editName = data.crisis.name;
      this.crisis = data.crisis;
    });
  }

  cancel() {
    this.gotoCrises();
  }

  save() {
    this.crisis.name = this.editName;
    this.gotoCrises();
  }
  gotoCrises() {
    let crisisId = this.crisis ? this.crisis.id : null;
    // Pass along the crisis id if available
    // so that the CrisisListComponent can select that crisis.
    // Add a totally useless `foo` parameter for kicks.
    // Relative navigation back to the crises
    this.router.navigate(['../', { id: crisisId, foo: 'foo' }], { relativeTo: this.route });
  }

  canDeactivate(): Observable<boolean> | boolean {
    // Allow synchronous navigation (`true`) if no crisis or the crisis is unchanged
    if (!this.crisis || this.crisis.name === this.editName) {
      return true;
    }
    // Otherwise ask the user with the dialog service and return its
    // observable which resolves to true or false when the user decides
    return this.dialogService.confirm('Discard changes?');
  }


}
