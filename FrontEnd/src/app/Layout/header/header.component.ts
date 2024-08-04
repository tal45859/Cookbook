import { Component, Input, OnInit ,HostListener } from '@angular/core';
import { NavBarMenultem } from 'src/app/Model/NavBarMenultem';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() HeaderNavBarData:Array<NavBarMenultem>=[];
  public responsiveWidthNav=true;
  constructor() { }

  ngOnInit(): void {
  }
    @HostListener('window:resize', ['$event'])
    onResize() {

      let navselment= document.getElementById("navs");
      if(window.innerWidth>991)
      {
        for(let i =0;i<this.HeaderNavBarData.length;i++)
        {
          let aelment= document.getElementById(""+this.HeaderNavBarData[i].name);
          aelment?.classList.add("text-white");
        }
        navselment?.classList.remove("bg-light");
      }
      else
      {
        for(let i =0;i<this.HeaderNavBarData.length;i++)
        {
          let aelment= document.getElementById(""+this.HeaderNavBarData[i].name);
          aelment?.classList.remove("text-white");
        }
        navselment?.classList.add("bg-light");
      }
    }
}
