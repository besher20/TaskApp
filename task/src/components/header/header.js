import { Component } from "react";
import './header.css'
class Header extends Component 
{
    render(props){
        return <div className="header">
            <h1 className="txt-h1">This is list of tasks</h1>
        </div>
    }
}
export default Header;