import React from 'react';

import EditForm from "./EditForm";

const MovieEdit = (props) => {
    
    return (
        <div>
            <h1>Cập nhật Phim</h1>
            <EditForm  
                requestUpdateMovie={props.requestUpdateMovie}
                createSession={props.createSession} 
                deleteSession={props.deleteSession} 
                movie={props.movie} 
                cinemas={props.cinemas}
            />
        </div>
    );
}

export default MovieEdit;