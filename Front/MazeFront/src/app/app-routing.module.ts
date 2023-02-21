import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { MazeMainComponent } from './components/maze-main/maze-main.component';
import { MazeViewComponent } from './components/maze-view/maze-view.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  {path:'login', component: LoginComponent},
  {path:'register', component: RegisterComponent},
  {path:'mazes', component: MazeViewComponent},
  {path: '', component: HomeComponent},
  {path:'**', component: MazeViewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
