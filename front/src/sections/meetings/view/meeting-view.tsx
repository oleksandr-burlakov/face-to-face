import React from "react"

import { Add } from "@mui/icons-material";
import { Box, Tab, Card, Tabs, Stack, Button, Container, Typography } from "@mui/material"

import ActiveList from "../active-list";

function CustomTabPanel(props : CustomTabPanelPropTypes) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

export type CustomTabPanelPropTypes = {
  children: any,
  index: number,
  value: number,
};

export function MeetingView() {
  const [value, setValue] = React.useState(0);

  const handleChange = (event: any, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Container>
      <Stack mb={5}>
        <Typography variant="h4">Meetings</Typography>
      </Stack>
      <Card>
        <Box padding={4}>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Stack flexDirection='row'>
              <Tabs sx={{width:'100%'}} value={value} onChange={handleChange} centered textColor="secondary" indicatorColor="secondary">
                <Tab label="Active"/>
                <Tab label="Archive"/>
              </Tabs>
              <Button color="secondary" variant="outlined">
                <Add />
              </Button>
            </Stack>
          </Box>
          <CustomTabPanel value={value} index={0}>
            <ActiveList />
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            Item Two
          </CustomTabPanel>
        </Box>
      </Card>
    </Container>
  )
}