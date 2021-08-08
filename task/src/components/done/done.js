import "./done.css";
function Done(props) {
    const done = props.done;
  
    return (
      <div className="done">
          <div className="content">
            <div className="task">{done}</div>
          </div>
      
      </div>
    );
  }

export default Done;
