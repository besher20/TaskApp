import "./App.css";
import { Component } from "react";
import Todo from "./components/todo/todo";
import Done from "./components/done/done";
import Header from "./components/header/header";
import axios from "axios";
import CreateTask from "./components/addTask/CreatTask";

async function GetTodoData() {
  try {
    const todo = await axios.get(
      `https://localhost:44365/api/Task/listCardToDo`,
      {
        headers: { "Access-Control-Allow-Origin": "*" },
      }
    );
    console.log(todo);

    return todo;
  } catch (error) {
    console.log(error);
  }
}
async function GetDoneData() {
  try {
    const done = await axios.get(
      `https://localhost:44365/api/Task/listCardDone`,
      {
        headers: { "Access-Control-Allow-Origin": "*" },
      }
    );
    return done;
  } catch (error) {
    console.log(error);
  }
}

class App extends Component {
  completeTask = (index) => {
    const todos = [...this.state.todos];
    const todo = todos[index];
    const done = [...this.state.done];
    done.push(todo.split(",")[0]);
    todos.splice(index, 1);
    const task =  axios.put(
      `https://localhost:44365/api/Task/doneTask?id=${todo.split(",")[1]}`);  
    this.setState({
      todos,
      done,
    });
  };
  state = {
    todos: [],
    done: [],
  };
  componentDidMount = () => {
    GetTodoData().then((response) => {
      this.setState({
        todos: response.data,
      });
    });
    GetDoneData().then((response) => {
      this.setState({
        done: response.data,
      });
    });
  };
  addTask = ()=>{
    const todos = [...this.state.todos];
    
    todos.push( this.state.newTask );
    const task =  axios.post(
      `https://localhost:44365/api/Task/addTask?name=${this.state.newTask}`);  
    this.setState({
      todos,
      newTask: '',
      id:'',
    });
  }
  
  updateNewTask = (event)=>{
    this.setState({
      newTask: event.target.value
    });
  }
  render() {
    return (
      <div className="bg-color">
        <Header> </Header>
        <CreateTask value={this.state.newTask} 
          onChange={this.updateNewTask} 
          addTask={this.addTask} />
        <div className="todo">
          <h3 className="txt-h3">To Do List</h3>
          {this.state.todos.map((todo, index) => (
            <Todo
              todo={todo}
              completeTask={() => this.completeTask(index)}
              key={index}
            />
          ))}
        </div>
        <div className="todo">
        <h3 className="txt-h3">Done List</h3>
        {this.state.done.map((done, index) => (
          <Done done={done} key={index} />
        ))}
        </div>
        
      </div>
    );
  }
}

export default App;
