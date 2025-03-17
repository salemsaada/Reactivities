import { Typography, List, ListItem, ListItemText } from "@mui/material"
import { useEffect, useState } from "react"
import axios from "axios";
function App() {
  type Activity =  {
    id: string
    title: string
    date: string
    description: string
    category: string
    isCancelled: boolean
    city: string
    venue: string
    latitude: number
    myProperty: number
    longitude: number
  }

const [activities, setActivities] = useState<Activity[]>([])

useEffect(() => {
  axios.get<Activity[]>('https://localhost:5001/api/activities/')
.then(response => setActivities(response.data))

return () => {}

},[])
  return (
    <>
      <Typography variant="h3">Reactivities</Typography>
      <List>
      {activities.map(activity => (
        <ListItem key={activity.id}>
          <ListItemText>{activity.title}</ListItemText>
        </ListItem>
      ))}
      </List>
    </>
  )
}

export default App

