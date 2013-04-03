<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:w="http://schemas.microsoft.com/wix/2006/wi">

  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="w:Component[w:File/@Source='$(var.BasePath)\azureftp-for-sharepoint.server.exe']">
    <w:Component>
      <xsl:attribute name="Id">
        <xsl:value-of select="@Id"/>
      </xsl:attribute>
      <xsl:attribute name="Guid">
        <xsl:value-of select="@Guid"/>
      </xsl:attribute>
      <xsl:attribute name="KeyPath">yes</xsl:attribute>
    </w:Component>
  </xsl:template>

</xsl:stylesheet>